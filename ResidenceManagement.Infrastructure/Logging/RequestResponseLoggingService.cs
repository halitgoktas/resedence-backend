using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using ResidenceManagement.Core.Interfaces.Logging;

namespace ResidenceManagement.Infrastructure.Logging
{
    /// <summary>
    /// HTTP istek ve yanıtlarını loglayan servis
    /// </summary>
    public class RequestResponseLoggingService : IRequestResponseLoggingService
    {
        private readonly ILoggingService _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        /// <summary>
        /// RequestResponseLoggingService constructor
        /// </summary>
        /// <param name="logger">Loglama servisi</param>
        public RequestResponseLoggingService(ILoggingService logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        /// <inheritdoc/>
        public async Task LogRequestAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            
            string requestBody = ReadStreamInChunks(requestStream);
            
            var requestInfo = new
            {
                IP = context.Connection.RemoteIpAddress,
                Schema = context.Request.Scheme,
                Host = context.Request.Host,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString,
                Method = context.Request.Method,
                ContentType = context.Request.ContentType,
                Headers = context.Request.Headers.Select(h => new { h.Key, Value = h.Value.ToString() }).ToList(),
                Cookies = context.Request.Cookies.Select(c => new { c.Key, c.Value }).ToList(),
                Body = requestBody
            };

            _logger.Info("HTTP Request Information: {@RequestInfo}", requestInfo);

            // Gövdeyi başlangıca geri sarmalıyız ki controller okuyabilsin
            context.Request.Body.Position = 0;
        }

        /// <inheritdoc/>
        public async Task LogResponseAsync(HttpContext context)
        {
            // Yanıt gövdesini okumak için orijinal yanıt akışını kaydetmemiz gerekli
            var originalBodyStream = context.Response.Body;

            await using var responseBodyStream = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBodyStream;

            // Yanıt içeriği yazıldıktan sonra (controller'dan ve downstream middleware'lerden gelen)
            // yanıt gövdesini okuyabilir ve loglarız
            var responseInfo = new
            {
                StatusCode = context.Response.StatusCode,
                ContentType = context.Response.ContentType,
                Headers = context.Response.Headers.Select(h => new { h.Key, Value = h.Value.ToString() }).ToList(),
                // Response cookies'i Select ile direkt okunamaz, bir dictionary oluşturuyoruz
                Cookies = GetResponseCookiesAsDictionary(context.Response.Cookies)
            };

            // ReadStreamInChunks metodu, büyük gövdelerle başa çıkmamıza yardımcı olur
            responseBodyStream.Position = 0;
            string responseBody = ReadStreamInChunks(responseBodyStream);
            _logger.Info("HTTP Response Information: {@ResponseInfo}, Body: {ResponseBody}", responseInfo, responseBody);

            // Yanıt gövdesini orijinal akışa kopyala ve istemciye göndermek için konum sıfırla
            responseBodyStream.Position = 0;
            await responseBodyStream.CopyToAsync(originalBodyStream);
        }

        private static Dictionary<string, string> GetResponseCookiesAsDictionary(IResponseCookies cookies)
        {
            // IResponseCookies direkt sorgulanamadığı için bu yöntemi kullanıyoruz
            // Gerçek implementasyonda HttpContext.Response.Headers üzerinden cookie'leri okuyabiliriz
            // ancak bu örnek için boş bir dictionary dönüyoruz
            return new Dictionary<string, string>();
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            // Performans için büyük içerikleri chunk'lara bölerek oku
            const int readChunkBufferLength = 4096;
            stream.Position = 0;
            
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            
            return textWriter.ToString();
        }
    }
} 