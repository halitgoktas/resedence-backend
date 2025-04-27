using ResidenceManagement.Core.Entities.Base;
using System;

namespace ResidenceManagement.Core.Entities
{
    // Aylık/dönemsel aidat tanımları için entity sınıfı
    public class DuesDefinition : BaseEntity
    {
        // Aidat tanımı adı (ör: Ocak 2023 Aidatı, 2023 Ocak-Şubat Aidatı)
        public string Name { get; set; }
        
        // Açıklama
        public string Description { get; set; }
        
        // Aidat tutarı
        public decimal Amount { get; set; }
        
        // Aidat dönemi başlangıç tarihi
        public DateTime StartDate { get; set; }
        
        // Aidat dönemi bitiş tarihi
        public DateTime EndDate { get; set; }
        
        // Son ödeme tarihi
        public DateTime DueDate { get; set; }
        
        // Gecikme cezası oranı (%)
        public decimal? LateFeePercentage { get; set; }
        
        // Gecikme cezası sabit tutarı
        public decimal? LateFeeAmount { get; set; }
        
        // Aidat hangi siteye ait
        public int ResidenceId { get; set; }
        
        // Site/Rezidans ilişkisi
        public virtual Residence Residence { get; set; }
        
        // Aidat tanımı aktif mi?
        public bool IsActive { get; set; } = true;
        
        // Aidat tanımı metrekare bazlı mı?
        public bool IsBasedOnSquareMeters { get; set; } = false;
        
        // Metrekare bazlı ise birim metrekare ücreti
        public decimal? SquareMeterPrice { get; set; }
    }
} 