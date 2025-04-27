namespace ResidenceManagement.Core.Common
{
    // API yanıtları için genel sınıf
    public class ApiResponse
    {
        // İşlem başarılı mı?
        public bool Success { get; set; }

        // Mesaj
        public string Message { get; set; }

        // HTTP durum kodu
        public int StatusCode { get; set; }

        // Veri (object tipinde)
        public object Data { get; set; }
    }

    // Generic veri tipi için API yanıt sınıfı
    public class ApiResponse<T> : ApiResponse
    {
        // Tipli veri
        public new T Data { get; set; }
    }
} 