using System;

namespace ResidenceManagement.Core.Exceptions
{
    // KBS entegrasyonu sırasında oluşan hataları temsil eden özel exception sınıfı
    public class KbsIntegrationException : Exception
    {
        // Varsayılan constructor
        public KbsIntegrationException() : base("KBS entegrasyonu sırasında bir hata oluştu.")
        {
        }

        // Mesaj parametreli constructor
        public KbsIntegrationException(string message) : base(message)
        {
        }

        // Mesaj ve iç exception parametreli constructor
        public KbsIntegrationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
} 