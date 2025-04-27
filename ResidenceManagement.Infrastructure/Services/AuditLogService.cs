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
    /// Denetim loglama servisi implementasyonu
    /// </summary>
    public class AuditLogService : IAuditLogService
    {
        private readonly ILogger<AuditLogService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// Yapıcı metot
        /// </summary>
        public AuditLogService(
            ILogger<AuditLogService> logger,
            IUnitOfWork unitOfWork,
            ApplicationDbContext dbContext)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task LogAsync(string userId, string controller, string action, object parameters, object result, string path)
        {
            try
            {
                var auditLog = new AuditLog
                {
                    UserId = userId,
                    Controller = controller,
                    Action = action,
                    Path = path,
                    LogDate = DateTime.UtcNow,
                    Parameters = JsonConvert.SerializeObject(parameters, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    }),
                    Result = JsonConvert.SerializeObject(result, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        NullValueHandling = NullValueHandling.Ignore
                    }),
                    IsSuccessful = true
                };

                await _unitOfWork.Repository<AuditLog>().AddAsync(auditLog);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Audit log could not be saved for user {UserId}, {Controller}.{Action}", userId, controller, action);
            }
        }

        /// <inheritdoc />
        public async Task<(IEnumerable<AuditLogDto> Logs, int TotalCount)> GetUserLogsAsync(
            string userId,
            DateTime startDate,
            DateTime endDate,
            int page = 1,
            int pageSize = 20)
        {
            // Multi-tenant filtrelemesini geçici olarak devre dışı bırak
            _dbContext.SetMultiTenantFilterEnabled(false);

            try
            {
                var query = _dbContext.Set<AuditLog>()
                    .Where(l => l.UserId == userId && l.LogDate >= startDate && l.LogDate <= endDate);

                var totalCount = await query.CountAsync();

                var logs = await query
                    .OrderByDescending(l => l.LogDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(l => new AuditLogDto
                    {
                        Id = l.Id,
                        Timestamp = l.LogDate,
                        UserId = l.UserId,
                        UserName = l.UserName,
                        Controller = l.Controller,
                        Action = l.Action,
                        Path = l.Path,
                        Parameters = l.Parameters,
                        Result = l.Result,
                        IpAddress = l.IpAddress
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
        public async Task<(IEnumerable<AuditLogDto> Logs, int TotalCount)> GetActionLogsAsync(
            string controller,
            string action,
            DateTime startDate,
            DateTime endDate,
            int page = 1,
            int pageSize = 20)
        {
            // Multi-tenant filtrelemesini geçici olarak devre dışı bırak
            _dbContext.SetMultiTenantFilterEnabled(false);

            try
            {
                var query = _dbContext.Set<AuditLog>()
                    .Where(l => l.Controller == controller && l.Action == action && l.LogDate >= startDate && l.LogDate <= endDate);

                var totalCount = await query.CountAsync();

                var logs = await query
                    .OrderByDescending(l => l.LogDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(l => new AuditLogDto
                    {
                        Id = l.Id,
                        Timestamp = l.LogDate,
                        UserId = l.UserId,
                        UserName = l.UserName,
                        Controller = l.Controller,
                        Action = l.Action,
                        Path = l.Path,
                        Parameters = l.Parameters,
                        Result = l.Result,
                        IpAddress = l.IpAddress
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
    }
} 