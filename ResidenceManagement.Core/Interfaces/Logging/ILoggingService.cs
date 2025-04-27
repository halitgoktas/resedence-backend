using System;

namespace ResidenceManagement.Core.Interfaces.Logging
{
    /// <summary>
    /// Loglama işlemleri için servis arayüzü
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Debug seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="message">Log mesajı</param>
        /// <param name="args">Mesaj için formatlanacak parametreler</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Exception ile birlikte debug seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="exception">Loglanacak istisna</param>
        /// <param name="message">Log mesajı</param>
        /// <param name="args">Mesaj için formatlanacak parametreler</param>
        void Debug(Exception exception, string message, params object[] args);

        /// <summary>
        /// Info seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="message">Log mesajı</param>
        /// <param name="args">Mesaj için formatlanacak parametreler</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Exception ile birlikte info seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="exception">Loglanacak istisna</param>
        /// <param name="message">Log mesajı</param>
        /// <param name="args">Mesaj için formatlanacak parametreler</param>
        void Info(Exception exception, string message, params object[] args);

        /// <summary>
        /// Warning seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="message">Log mesajı</param>
        /// <param name="args">Mesaj için formatlanacak parametreler</param>
        void Warning(string message, params object[] args);

        /// <summary>
        /// Exception ile birlikte warning seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="exception">Loglanacak istisna</param>
        /// <param name="message">Log mesajı</param>
        /// <param name="args">Mesaj için formatlanacak parametreler</param>
        void Warning(Exception exception, string message, params object[] args);

        /// <summary>
        /// Error seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="message">Log mesajı</param>
        /// <param name="args">Mesaj için formatlanacak parametreler</param>
        void Error(string message, params object[] args);

        /// <summary>
        /// Exception ile birlikte error seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="exception">Loglanacak istisna</param>
        /// <param name="message">Log mesajı</param>
        /// <param name="args">Mesaj için formatlanacak parametreler</param>
        void Error(Exception exception, string message, params object[] args);

        /// <summary>
        /// Fatal seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="message">Log mesajı</param>
        /// <param name="args">Mesaj için formatlanacak parametreler</param>
        void Fatal(string message, params object[] args);

        /// <summary>
        /// Exception ile birlikte fatal seviyesinde log kaydı oluşturur
        /// </summary>
        /// <param name="exception">Loglanacak istisna</param>
        /// <param name="message">Log mesajı</param>
        /// <param name="args">Mesaj için formatlanacak parametreler</param>
        void Fatal(Exception exception, string message, params object[] args);
    }
} 