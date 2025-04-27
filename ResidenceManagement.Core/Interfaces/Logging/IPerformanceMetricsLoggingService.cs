using System;
using System.Threading.Tasks;

namespace ResidenceManagement.Core.Interfaces.Logging
{
    /// <summary>
    /// Performans metriklerini loglayan servis arayüzü
    /// </summary>
    public interface IPerformanceMetricsLoggingService
    {
        /// <summary>
        /// Bir operasyon için performans ölçümünü başlatır
        /// </summary>
        /// <param name="operationName">Operasyon adı</param>
        void StartMeasurement(string operationName);

        /// <summary>
        /// Bir operasyon için performans ölçümünü sonlandırır ve loglar
        /// </summary>
        /// <param name="operationName">Operasyon adı</param>
        /// <param name="additionalInfo">Ek bilgi</param>
        void EndMeasurement(string operationName, string additionalInfo = null);

        /// <summary>
        /// Bir operasyonun çalışma süresini ölçer ve loglar
        /// </summary>
        /// <typeparam name="T">Dönüş tipi</typeparam>
        /// <param name="operationName">Operasyon adı</param>
        /// <param name="operation">Ölçülecek operasyon</param>
        /// <param name="additionalInfo">Ek bilgi</param>
        /// <returns>Operasyonun dönüş değeri</returns>
        T MeasureExecutionTime<T>(string operationName, Func<T> operation, string additionalInfo = null);

        /// <summary>
        /// Asenkron bir operasyonun çalışma süresini ölçer ve loglar
        /// </summary>
        /// <typeparam name="T">Dönüş tipi</typeparam>
        /// <param name="operationName">Operasyon adı</param>
        /// <param name="operation">Ölçülecek asenkron operasyon</param>
        /// <param name="additionalInfo">Ek bilgi</param>
        /// <returns>Operasyonun dönüş değeri</returns>
        Task<T> MeasureExecutionTimeAsync<T>(string operationName, Func<Task<T>> operation, string additionalInfo = null);

        /// <summary>
        /// Bir operasyonun çalışma süresini ölçer ve loglar (dönüş değeri olmayan)
        /// </summary>
        /// <param name="operationName">Operasyon adı</param>
        /// <param name="operation">Ölçülecek operasyon</param>
        /// <param name="additionalInfo">Ek bilgi</param>
        void MeasureExecutionTime(string operationName, Action operation, string additionalInfo = null);

        /// <summary>
        /// Asenkron bir operasyonun çalışma süresini ölçer ve loglar (dönüş değeri olmayan)
        /// </summary>
        /// <param name="operationName">Operasyon adı</param>
        /// <param name="operation">Ölçülecek asenkron operasyon</param>
        /// <param name="additionalInfo">Ek bilgi</param>
        Task MeasureExecutionTimeAsync(string operationName, Func<Task> operation, string additionalInfo = null);
    }
} 