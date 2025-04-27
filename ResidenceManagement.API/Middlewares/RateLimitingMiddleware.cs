using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ResidenceManagement.Core.Common;

namespace ResidenceManagement.API.Middlewares
{
    /// <summary>
    /// API rate limiting için konfigürasyon sınıfı
    /// </summary>
    public class RateLimitingOptions
    {
        public bool Enabled { get; set; } = true;
        public bool EnableIpRateLimiting { get; set; } = true;
        public bool EnableClientIdRateLimiting { get; set; } = false;
        public bool EnableEndpointRateLimiting { get; set; } = false;
        public string ClientIdHeader { get; set; } = "X-ClientId";
        public int DefaultRequestLimit { get; set; } = 100;
        public int DefaultTimeWindowInMinutes { get; set; } = 1;
        public List<RateLimitRule> Rules { get; set; } = new List<RateLimitRule>();
    }

    /// <summary>
    /// Rate limit kuralı
    /// </summary>
    public class RateLimitRule
    {
        public string Endpoint { get; set; }
        public int Limit { get; set; }
        public int TimeWindowInMinutes { get; set; }
    }

    /// <summary>
    /// API Rate Limiting Middleware
    /// </summary>
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RateLimitingMiddleware> _logger;
        private readonly IDistributedCache _cache;
        private readonly RateLimitingOptions _options;

        public RateLimitingMiddleware(
            RequestDelegate next,
            ILogger<RateLimitingMiddleware> logger,
            IDistributedCache cache,
            IOptions<RateLimitingOptions> options)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!_options.Enabled)
            {
                await _next(context);
                return;
            }

            var endpoint = context.Request.Path.Value?.ToLower();
            var clientId = GetClientIdentifier(context);
            var rule = GetMatchingRule(endpoint);

            var limit = rule?.Limit ?? _options.DefaultRequestLimit;
            var timeWindow = rule?.TimeWindowInMinutes ?? _options.DefaultTimeWindowInMinutes;

            // Cache anahtarı oluştur
            string cacheKey = $"rl:{clientId}:{endpoint}";

            // Mevcut istek sayısını cache'den al
            var cachedData = await _cache.GetStringAsync(cacheKey);
            var currentRequests = string.IsNullOrEmpty(cachedData) ? 0 : int.Parse(cachedData);

            // Eğer limit aşıldıysa, 429 Too Many Requests hatası döndür
            if (currentRequests >= limit)
            {
                _logger.LogWarning("Rate limit aşıldı: {ClientId}, {Endpoint}, Limit: {Limit}", clientId, endpoint, limit);
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                context.Response.Headers.Add("Retry-After", timeWindow.ToString());

                // API Response formatında hata yanıtı oluştur
                var response = new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = $"Çok fazla istek gönderildi. Lütfen {timeWindow} dakika sonra tekrar deneyin.",
                    StatusCode = HttpStatusCode.TooManyRequests
                };

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }));

                return;
            }

            // İstek sayısını artır ve cache'i güncelle
            await _cache.SetStringAsync(
                cacheKey,
                (currentRequests + 1).ToString(),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(timeWindow)
                });

            // Kalan istek sayısını header'a ekle
            context.Response.Headers.Add("X-RateLimit-Limit", limit.ToString());
            context.Response.Headers.Add("X-RateLimit-Remaining", (limit - currentRequests - 1).ToString());
            context.Response.Headers.Add("X-RateLimit-Reset", DateTimeOffset.UtcNow.AddMinutes(timeWindow).ToUnixTimeSeconds().ToString());

            await _next(context);
        }

        /// <summary>
        /// Client tanımlayıcısını al (IP adresi veya clientId)
        /// </summary>
        private string GetClientIdentifier(HttpContext context)
        {
            if (_options.EnableClientIdRateLimiting && 
                context.Request.Headers.TryGetValue(_options.ClientIdHeader, out var clientId) && 
                !string.IsNullOrEmpty(clientId))
            {
                return clientId;
            }

            if (_options.EnableIpRateLimiting)
            {
                var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                return $"ip:{ip}";
            }

            return "global";
        }

        /// <summary>
        /// Endpoint için uygun kuralı bul
        /// </summary>
        private RateLimitRule? GetMatchingRule(string endpoint)
        {
            if (!_options.EnableEndpointRateLimiting || string.IsNullOrEmpty(endpoint))
                return null;

            return _options.Rules
                .FirstOrDefault(r => endpoint.StartsWith(r.Endpoint, StringComparison.OrdinalIgnoreCase));
        }
    }
} 