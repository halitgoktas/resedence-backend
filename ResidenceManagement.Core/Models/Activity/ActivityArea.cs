using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Models.Activity
{
    /// <summary>
    /// Rezidans ve sitelerde bulunan ortak aktivite alanlarını tanımlayan sınıf
    /// </summary>
    public class ActivityArea : BaseEntity
    {
        /// <summary>
        /// Aktivite alanının adı (ör. Yüzme Havuzu, Fitness Salonu, Toplantı Odası)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Aktivite alanının tipi (ör. Spor, Sosyal, Eğlence)
        /// </summary>
        public string AreaType { get; set; }

        /// <summary>
        /// Aktivite alanının detaylı açıklaması
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Aktivite alanının bulunduğu rezidans veya site ID'si
        /// </summary>
        public Guid ResidenceId { get; set; }

        /// <summary>
        /// Aktivite alanının bulunduğu blok ID'si (varsa)
        /// </summary>
        public Guid? BlockId { get; set; }

        /// <summary>
        /// Alanın konumu (ör. "A Blok Zemin Kat", "B Blok 2. Kat")
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Alanın kapasitesi (maksimum kişi sayısı)
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Alanın boyutu (metrekare)
        /// </summary>
        public decimal Size { get; set; }

        /// <summary>
        /// Fotoğraf veya görsel dosyalarının yolları (JSON formatında dizi)
        /// </summary>
        public string Images { get; set; }

        /// <summary>
        /// Alanın açılış saati (24 saat formatında: 08:00)
        /// </summary>
        public TimeSpan OpeningTime { get; set; }

        /// <summary>
        /// Alanın kapanış saati (24 saat formatında: 22:00)
        /// </summary>
        public TimeSpan ClosingTime { get; set; }

        /// <summary>
        /// Alan rezervasyonla mı kullanılıyor
        /// </summary>
        public bool RequiresReservation { get; set; }

        /// <summary>
        /// Alanın minimum rezervasyon süresi (saat)
        /// </summary>
        public int MinimumReservationHours { get; set; }

        /// <summary>
        /// Alanın maksimum rezervasyon süresi (saat)
        /// </summary>
        public int MaximumReservationHours { get; set; }

        /// <summary>
        /// Rezervasyonlar arası temizlik süresi (dakika)
        /// </summary>
        public int CleanupTimeMinutes { get; set; }

        /// <summary>
        /// Alan ücretli mi
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Saatlik kullanım ücreti
        /// </summary>
        public decimal HourlyRate { get; set; }

        /// <summary>
        /// Günlük kullanım ücreti
        /// </summary>
        public decimal DailyRate { get; set; }

        /// <summary>
        /// Alan kaç gün önceden rezerve edilebilir
        /// </summary>
        public int AdvanceReservationDays { get; set; }

        /// <summary>
        /// Alanı rezerve edebilecek minimum yaş
        /// </summary>
        public int MinimumAgeRequirement { get; set; }

        /// <summary>
        /// Alanın şu anda kullanılabilir olup olmadığı
        /// </summary>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Alanın kullanılamama nedeni (bakım, onarım, vb.)
        /// </summary>
        public string UnavailabilityReason { get; set; }

        /// <summary>
        /// Alanın tekrar kullanılabilir olacağı tarih (bakım veya onarım durumunda)
        /// </summary>
        public DateTime? AvailableAgainDate { get; set; }

        /// <summary>
        /// Alanda bulunan özellikler ve ekipmanlar (JSON formatında dizi)
        /// </summary>
        public string Amenities { get; set; }

        /// <summary>
        /// Kullanım kuralları
        /// </summary>
        public string Rules { get; set; }

        /// <summary>
        /// Güvenlik önlemleri
        /// </summary>
        public string SafetyMeasures { get; set; }

        /// <summary>
        /// Bu aktivite alanı için yapılmış rezervasyonlar
        /// </summary>
        public virtual ICollection<ActivityReservation> Reservations { get; set; }

        /// <summary>
        /// Alanın kullanım istatistikleri (JSON formatında - ay, gün, saat bazında kullanım yoğunluğu)
        /// </summary>
        public string UsageStatistics { get; set; }

        /// <summary>
        /// Alanın bakım planı (JSON formatında - yapılacak bakımlar ve tarihleri)
        /// </summary>
        public string MaintenanceSchedule { get; set; }

        public ActivityArea()
        {
            Reservations = new List<ActivityReservation>();
            OpeningTime = new TimeSpan(8, 0, 0); // 08:00
            ClosingTime = new TimeSpan(22, 0, 0); // 22:00
            IsAvailable = true;
            MinimumReservationHours = 1;
            MaximumReservationHours = 4;
            AdvanceReservationDays = 7;
            CleanupTimeMinutes = 30;
            MinimumAgeRequirement = 18;
        }

        /// <summary>
        /// Belirtilen tarih ve saat aralığında alanın müsait olup olmadığını kontrol eder
        /// </summary>
        /// <param name="startDateTime">Başlangıç tarih ve saati</param>
        /// <param name="endDateTime">Bitiş tarih ve saati</param>
        /// <returns>Müsaitlik durumu</returns>
        public bool CheckAvailability(DateTime startDateTime, DateTime endDateTime)
        {
            // Alanın genel müsaitlik durumunu kontrol et
            if (!IsAvailable)
                return false;

            // Çalışma saatlerini kontrol et
            TimeSpan startTime = startDateTime.TimeOfDay;
            TimeSpan endTime = endDateTime.TimeOfDay;

            if (startTime < OpeningTime || endTime > ClosingTime)
                return false;

            // Rezervasyon süresi kontrolü
            TimeSpan duration = endDateTime - startDateTime;
            double hours = duration.TotalHours;

            if (hours < MinimumReservationHours || hours > MaximumReservationHours)
                return false;

            // Mevcut rezervasyonlara göre müsaitlik kontrolü yapılmalı
            // Bu kısım, rezervasyon listesine erişim gerektirir ve genellikle servis katmanında yapılır
            
            return true;
        }

        /// <summary>
        /// Belirtilen tarih ve saat aralığı için kullanım ücretini hesaplar
        /// </summary>
        /// <param name="startDateTime">Başlangıç tarih ve saati</param>
        /// <param name="endDateTime">Bitiş tarih ve saati</param>
        /// <returns>Hesaplanan ücret</returns>
        public decimal CalculateUsageFee(DateTime startDateTime, DateTime endDateTime)
        {
            if (!IsPaid)
                return 0;

            TimeSpan duration = endDateTime - startDateTime;
            double hours = duration.TotalHours;
            double days = duration.TotalDays;

            // Eğer 24 saatten fazla ise günlük ücret, değilse saatlik ücret üzerinden hesapla
            if (days >= 1)
            {
                return (decimal)Math.Ceiling(days) * DailyRate;
            }
            else
            {
                return (decimal)Math.Ceiling(hours) * HourlyRate;
            }
        }
    }
} 