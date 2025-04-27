using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.Common;

namespace ResidenceManagement.API.Middlewares
{
    /// <summary>
    /// Tüm API isteklerindeki hataları yakalar ve standart bir formatta yanıt döndürür.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Beklenmeyen bir hata oluştu: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = new ApiResponse<object>
            {
                Success = false,
                Data = null
            };

            var errorCode = ErrorCodes.GeneralError;
            var statusCode = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case UnauthorizedAccessException:
                    errorCode = ErrorCodes.NotAuthorized;
                    statusCode = HttpStatusCode.Unauthorized;
                    response.Message = ErrorCodes.GetErrorMessage(errorCode);
                    break;

                case ArgumentException:
                    errorCode = ErrorCodes.InvalidData;
                    statusCode = HttpStatusCode.BadRequest;
                    response.Message = exception.Message;
                    break;

                case KeyNotFoundException:
                    errorCode = ErrorCodes.ResourceNotFound;
                    statusCode = HttpStatusCode.NotFound;
                    response.Message = ErrorCodes.GetErrorMessage(errorCode);
                    break;

                case BusinessRuleException businessEx:
                    errorCode = businessEx.ErrorCode;
                    statusCode = HttpStatusCode.UnprocessableEntity;
                    response.Message = businessEx.Message;
                    response.Errors = businessEx.Errors;
                    break;

                default:
                    response.Message = ErrorCodes.GetErrorMessage(errorCode);
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            response.StatusCode = (int)statusCode;
            response.ErrorCode = errorCode;

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, jsonOptions);
            await context.Response.WriteAsync(json);
        }
    }
} 