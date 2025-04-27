using System;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.Interfaces.Logging;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace ResidenceManagement.Infrastructure.Logging
{
    /// <summary>
    /// Serilog tabanlı loglama servisi implementasyonu
    /// </summary>
    public class SerilogService : ILoggingService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// SerilogService constructor
        /// </summary>
        /// <param name="logger">Serilog logger</param>
        public SerilogService(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        /// <inheritdoc/>
        public void Debug(Exception exception, string message, params object[] args)
        {
            _logger.Debug(exception, message, args);
        }

        /// <inheritdoc/>
        public void Info(string message, params object[] args)
        {
            _logger.Information(message, args);
        }

        /// <inheritdoc/>
        public void Info(Exception exception, string message, params object[] args)
        {
            _logger.Information(exception, message, args);
        }

        /// <inheritdoc/>
        public void Warning(string message, params object[] args)
        {
            _logger.Warning(message, args);
        }

        /// <inheritdoc/>
        public void Warning(Exception exception, string message, params object[] args)
        {
            _logger.Warning(exception, message, args);
        }

        /// <inheritdoc/>
        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        /// <inheritdoc/>
        public void Error(Exception exception, string message, params object[] args)
        {
            _logger.Error(exception, message, args);
        }

        /// <inheritdoc/>
        public void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }

        /// <inheritdoc/>
        public void Fatal(Exception exception, string message, params object[] args)
        {
            _logger.Fatal(exception, message, args);
        }
    }
} 