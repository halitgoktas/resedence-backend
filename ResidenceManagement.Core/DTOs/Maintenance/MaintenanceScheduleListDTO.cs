using System;

namespace ResidenceManagement.Core.DTOs.Maintenance
{
    // Bakım planı liste görünümü için DTO sınıfı
    public class MaintenanceScheduleListDTO
    {
        // Bakım planı ID'si
        public int Id { get; set; }

        // Bakım başlığı
        public string Title { get; set; }

        // Bakım açıklaması
        public string Description { get; set; }

        // Bakım tipi
        public string MaintenanceType { get; set; }

        // Öncelik seviyesi
        public string Priority { get; set; }

        // Planlanan tarih
        public DateTime ScheduledDate { get; set; }

        // Planlanan saat
        public TimeSpan? ScheduledTime { get; set; }

        // Tahmini süre (dakika)
        public int EstimatedDurationMinutes { get; set; }

        // Durum
        public string Status { get; set; }

        // Atanan kullanıcı ID'si
        public int? AssignedToUserId { get; set; }

        // Atanan kullanıcı adı
        public string AssignedToUserName { get; set; }

        // Lokasyon bilgileri
        public int? ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        public int? ApartmentId { get; set; }
        public string ApartmentNumber { get; set; }

        // Tekrarlayan bakım mı?
        public bool IsRecurring { get; set; }

        // Tahmini maliyet
        public decimal EstimatedCost { get; set; }

        // Firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }

        // Oluşturulma tarihi
        public DateTime CreatedDate { get; set; }

        // Son güncelleme tarihi
        public DateTime? UpdatedDate { get; set; }
    }
} 