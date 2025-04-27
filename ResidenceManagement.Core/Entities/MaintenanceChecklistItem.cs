using System;
using ResidenceManagement.Core.Entities.Base;

namespace ResidenceManagement.Core.Entities
{
    /// <summary>
    /// Bakım kontrol listesi öğesi entity sınıfı
    /// </summary>
    public class MaintenanceChecklistItem : BaseEntity
    {
        /// <summary>
        /// Bakım kontrol listesi öğesinin başlığı
        /// </summary>
        public string Title { get; set; } = null!;

        /// <summary>
        /// Bakım kontrol listesi öğesinin açıklaması
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Bakım kontrol listesi öğesinin zorunlu olup olmadığı
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Bakım kontrol listesi öğesine ait notlar
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Bakım kontrol listesi öğesinin tamamlanıp tamamlanmadığı
        /// </summary>
        public bool IsCompleted { get; set; }

        /// <summary>
        /// Bakım kontrol listesi öğesinin tamamlandığı tarih
        /// </summary>
        public DateTime? CompletedDate { get; set; }
        
        /// <summary>
        /// Bakım kontrol listesi öğesinin planlanan tamamlanma tarihi
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// Bakım kontrol listesi öğesini tamamlayan kullanıcının ID'si
        /// </summary>
        public int? CompletedByUserId { get; set; }

        /// <summary>
        /// Bakım kontrol listesi öğesinin sırası (veritabanında saklanır)
        /// </summary>
        public int OrderNumber { get; set; }
        
        /// <summary>
        /// Bakım kontrol listesi öğesinin sırası (UI için kullanılır)
        /// OrderNumber değeri ile senkronize çalışır, veritabanında saklanmaz
        /// </summary>
        public int OrderIndex 
        { 
            get { return OrderNumber; }
            set { OrderNumber = value; }
        }

        /// <summary>
        /// Bakım planının ID'si
        /// </summary>
        public int MaintenanceScheduleId { get; set; }

        /// <summary>
        /// Bakım kontrol listesi öğesinin ait olduğu bakım planı
        /// </summary>
        public MaintenanceSchedule MaintenanceSchedule { get; set; } = null!;

        /// <summary>
        /// Bakım kontrol listesi öğesini tamamlayan kullanıcı
        /// </summary>
        public User? CompletedByUser { get; set; }
    }
} 