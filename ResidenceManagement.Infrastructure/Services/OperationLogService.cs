using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ResidenceManagement.Core.Entities.Logging;
using ResidenceManagement.Core.Interfaces.Repositories;
using ResidenceManagement.Core.Services;
using ResidenceManagement.Infrastructure.Data.Context;

namespace ResidenceManagement.Infrastructure.Services
{
    /// <summary>
    /// Operasyon loglama servisi implementasyonu
    /// </summary>
    public class OperationLogService : IOperationLogService
    {
        private readonly ILogger<OperationLogService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Yapıcı metot
        /// </summary>
        public OperationLogService(
            ILogger<OperationLogService> logger,
            IUnitOfWork unitOfWork,
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task LogAsync(string operationType, string message, object details = null)
        {
            await LogInternalAsync(operationType, message, null, details);
        }

        /// <inheritdoc />
        public async Task LogAsync(int userId, string operationType, string message, object details = null)
        {
            await LogInternalAsync(operationType, message, userId, details);
        }

        /// <inheritdoc />
        public async Task<(IEnumerable<OperationLogDto> Logs, int TotalCount)> GetLogsAsync(
            DateTime startDate,
            DateTime endDate,
            string operationType = null,
            int? userId = null,
            int page = 1,
            int pageSize = 20)
        {
            // Multi-tenant filtrelemesini geçici olarak devre dışı bırak
            _dbContext.SetMultiTenantFilterEnabled(false);

            try
            {
                var query = _dbContext.Set<OperationLog>()
                    .Where(l => l.LogDate >= startDate && l.LogDate <= endDate);

                if (!string.IsNullOrEmpty(operationType))
                {
                    query = query.Where(l => l.OperationType == operationType);
                }

                if (userId.HasValue)
                {
                    query = query.Where(l => l.UserId == userId);
                }

                var totalCount = await query.CountAsync();

                var logs = await query
                    .OrderByDescending(l => l.LogDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(l => new OperationLogDto
                    {
                        Id = l.Id,
                        Timestamp = l.LogDate,
                        OperationType = l.OperationType,
                        Message = l.Message,
                        UserId = l.UserId,
                        Details = l.Details
                    })
                    .ToListAsync();

                return (logs, totalCount);
            }
            finally
            {
                // Multi-tenant filtrelemesini tekrar etkinleştir
                _dbContext.SetMultiTenantFilterEnabled(true);
            }
        }

        /// <inheritdoc />
        public async Task LogErrorAsync(Exception exception, string message, int? userId = null)
        {
            var details = new
            {
                ExceptionType = exception.GetType().Name,
                ExceptionMessage = exception.Message,
                StackTrace = exception.StackTrace,
                InnerException = exception.InnerException?.Message
            };

            await LogInternalAsync(
                "Error",
                message,
                userId,
                details,
                ResidenceManagement.Core.Entities.Logging.LogLevel.Error);

            // Ayrıca Serilog ile de logla
            _logger.LogError(exception, message);
        }

        /// <summary>
        /// Operasyon logu kaydeden yardımcı metot
        /// </summary>
        private async Task LogInternalAsync(
            string operationType,
            string message,
            int? userId,
            object details,
            ResidenceManagement.Core.Entities.Logging.LogLevel logLevel = ResidenceManagement.Core.Entities.Logging.LogLevel.Information)
        {
            try
            {
                var operationLog = new OperationLog
                {
                    OperationType = operationType,
                    Message = message,
                    UserId = userId,
                    LogDate = DateTime.UtcNow,
                    Details = details != null ? JsonConvert.SerializeObject(details) : null,
                    LogLevel = logLevel,
                    MachineName = Environment.MachineName,
                    ProcessId = System.Diagnostics.Process.GetCurrentProcess().Id.ToString()
                };

                await _unitOfWork.Repository<OperationLog>().AddAsync(operationLog);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Loglama işleminde hata olursa, sadece Serilog ile logla
                _logger.LogWarning(ex, "Operation log could not be saved: {Message}", message);
            }
        }
    }
} 