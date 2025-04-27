using System;
using ResidenceManagement.Core.Models.Common;

namespace ResidenceManagement.Core.Models.Staff
{
    // Personel devam takip bilgilerini temsil eden entity sınıfı
    public class StaffAttendance : BaseEntity
    {
        public int StaffId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public TimeSpan? BreakStartTime { get; set; }
        public TimeSpan? BreakEndTime { get; set; }
        public string AttendanceStatus { get; set; } // Tam Gün, Yarım Gün, İzinli, Raporlu, Geç Geldi, Erken Çıktı
        public bool IsPresent { get; set; }
        public decimal WorkedHours { get; set; }
        public decimal OvertimeHours { get; set; }
        public string Location { get; set; } // Personelin çalıştığı lokasyon
        public string Notes { get; set; }
        public string AuthorizedBy { get; set; } // İzin veren kişi
        
        // Multi-tenant alanları
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // İlişkiler
        public virtual Staff Staff { get; set; }
    }
} 