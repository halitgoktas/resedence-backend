using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ResidenceManagement.Core.Interfaces.Logging;

namespace ResidenceManagement.Infrastructure.Logging
{
    /// <summary>
    /// Performans metriklerini loglayan servis
    /// </summary>
    public class PerformanceMetricsLoggingService : IPerformanceMetricsLoggingService
    {
        private readonly ILoggingService _logger;
        private readonly ConcurrentDictionary<string, Stopwatch> _runningStopwatches;

        /// <summary>
        /// PerformanceMetricsLoggingService constructor
        /// </summary>
        /// <param name="logger">Loglama servisi</param>
        public PerformanceMetricsLoggingService(ILoggingService logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _runningStopwatches = new ConcurrentDictionary<string, Stopwatch>();
        }

        /// <inheritdoc/>
        public void StartMeasurement(string operationName)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            _runningStopwatches[operationName] = stopwatch;
            _logger.Debug("Performans ölçümü başlatıldı: {OperationName}", operationName);
        }

        /// <inheritdoc/>
        public void EndMeasurement(string operationName, string additionalInfo = null)
        {
            if (_runningStopwatches.TryRemove(operationName, out Stopwatch stopwatch))
            {
                stopwatch.Stop();
                var elapsed = stopwatch.Elapsed;

                var logMessage = $"Performans metriği: {operationName} - {elapsed.TotalMilliseconds:F2} ms";
                if (!string.IsNullOrEmpty(additionalInfo))
                {
                    logMessage += $" - {additionalInfo}";
                }

                // Kritik performans eşiğini tanımla (örneğin 1 saniye)
                if (elapsed.TotalMilliseconds > 1000)
                {
                    _logger.Warning(logMessage);
                }
                else
                {
                    _logger.Info(logMessage);
                }
            }
            else
            {
                _logger.Warning("Performans ölçümü kaydı bulunamadı: {OperationName}", operationName);
            }
        }

        /// <inheritdoc/>
        public T MeasureExecutionTime<T>(string operationName, Func<T> operation, string additionalInfo = null)
        {
            StartMeasurement(operationName);
            try
            {
                return operation();
            }
            finally
            {
                EndMeasurement(operationName, additionalInfo);
            }
        }

        /// <inheritdoc/>
        public async Task<T> MeasureExecutionTimeAsync<T>(string operationName, Func<Task<T>> operation, string additionalInfo = null)
        {
            StartMeasurement(operationName);
            try
            {
                return await operation();
            }
            finally
            {
                EndMeasurement(operationName, additionalInfo);
            }
        }

        /// <inheritdoc/>
        public void MeasureExecutionTime(string operationName, Action operation, string additionalInfo = null)
        {
            StartMeasurement(operationName);
            try
            {
                operation();
            }
            finally
            {
                EndMeasurement(operationName, additionalInfo);
            }
        }

        /// <inheritdoc/>
        public async Task MeasureExecutionTimeAsync(string operationName, Func<Task> operation, string additionalInfo = null)
        {
            StartMeasurement(operationName);
            try
            {
                await operation();
            }
            finally
            {
                EndMeasurement(operationName, additionalInfo);
            }
        }
    }
} 