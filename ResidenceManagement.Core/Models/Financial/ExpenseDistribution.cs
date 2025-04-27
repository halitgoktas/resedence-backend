using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities;

namespace ResidenceManagement.Core.Models.Financial
{
    // Giderlerin dairelere veya blok/binalara dağıtımını temsil eden sınıf
    public class ExpenseDistribution
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // İlişkili gider işlemi
        public int ExpenseTransactionId { get; set; }
        public ExpenseTransaction ExpenseTransaction { get; set; }
        
        // Dağıtım tarihi
        public DateTime DistributionDate { get; set; }
        
        // Dağıtım tipi
        public DistributionType DistributionType { get; set; }
        
        // Dağıtım hedefi (daire veya blok/bina)
        public DistributionTarget TargetType { get; set; }
        
        // Hedef ID (daire veya blok/bina ID'si)
        public int? ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        
        public int? BlockId { get; set; }
        public Block Block { get; set; }
        
        // Dağıtılan tutar
        public decimal Amount { get; set; }
        
        // Dağıtım oranı veya katsayısı
        public decimal DistributionFactor { get; set; }
        
        // Dağıtım açıklaması
        public string Description { get; set; }
        
        // Fatura oluşturuldu mu?
        public bool IsInvoiced { get; set; }
        
        // İlgili fatura ID (oluşturulduysa)
        public int? InvoiceId { get; set; }
        
        // Dağıtım durumu
        public DistributionStatus Status { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        public ExpenseDistribution()
        {
            DistributionDate = DateTime.Now;
            Status = DistributionStatus.Pending;
            IsInvoiced = false;
        }
    }
    
    // Dağıtım tipi
    public enum DistributionType
    {
        // Eşit olarak dağıtım
        Equal = 0,
        
        // Metrekareye göre dağıtım
        ByArea = 1,
        
        // Kişi sayısına göre dağıtım
        ByPersonCount = 2,
        
        // Kullanım oranına göre dağıtım
        ByUsageRatio = 3,
        
        // Özel oran ile dağıtım
        ByCustomRatio = 4,
        
        // Diğer dağıtım metotları
        Other = 5
    }
    
    // Dağıtım hedefi
    public enum DistributionTarget
    {
        // Daire bazlı dağıtım
        Apartment = 0,
        
        // Blok/bina bazlı dağıtım
        Block = 1,
        
        // Tüm site bazlı dağıtım
        AllResidence = 2
    }
    
    // Dağıtım durumu
    public enum DistributionStatus
    {
        // Beklemede
        Pending = 0,
        
        // İşleniyor
        Processing = 1,
        
        // Tamamlandı
        Completed = 2,
        
        // İptal edildi
        Cancelled = 3,
        
        // Hatalı
        Failed = 4
    }
} 