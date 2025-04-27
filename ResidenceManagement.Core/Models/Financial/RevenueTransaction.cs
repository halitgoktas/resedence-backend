using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Enums;

namespace ResidenceManagement.Core.Models.Financial
{
    // Gelir işlemlerini temsil eden sınıf
    public class RevenueTransaction
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Firma ve şube bilgileri (multi-tenant yapı için)
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // İlişkili rezidans/site bilgisi
        public int ResidenceId { get; set; }
        public string ResidenceName { get; set; }
        
        // İlişkili daire bilgisi
        public int ApartmentId { get; set; }
        public string ApartmentNumber { get; set; }
        public string BlockName { get; set; }
        public int FloorNumber { get; set; }
        
        // İşlem tipi
        public RevenueTransactionType TransactionType { get; set; }
        
        // Gelir kategorisi
        public int RevenueCategoryId { get; set; }
        public string RevenueCategoryName { get; set; }
        
        // İşlem tarihi
        public DateTime TransactionDate { get; set; }
        
        // İşlem açıklaması
        public string Description { get; set; }
        
        // Gelir tutarı
        public decimal Amount { get; set; }
        
        // Para birimi
        public string Currency { get; set; }
        
        // Kullanılan döviz kuru (TL dışındaki para birimleri için)
        public decimal? ExchangeRate { get; set; }
        
        // TL cinsinden tutar
        public decimal TRYAmount { get; set; }
        
        // KDV oranı (%)
        public decimal VATRate { get; set; }
        
        // KDV tutarı
        public decimal VATAmount { get; set; }
        
        // Stopaj oranı (%)
        public decimal WithholdingTaxRate { get; set; }
        
        // Stopaj tutarı
        public decimal WithholdingTaxAmount { get; set; }
        
        // Yönetim komisyonu oranı (%)
        public decimal ManagementCommissionRate { get; set; }
        
        // Yönetim komisyonu tutarı
        public decimal ManagementCommissionAmount { get; set; }
        
        // Yönetim komisyonu KDV tutarı
        public decimal ManagementCommissionVATAmount { get; set; }
        
        // Net tutar (daire sahibinin alacağı)
        public decimal NetAmount { get; set; }
        
        // Ödeme durumu
        public PaymentStatus PaymentStatus { get; set; }
        
        // Ödeme tarihi
        public DateTime? PaymentDate { get; set; }
        
        // Ödeme yöntemi
        public PaymentMethod? PaymentMethod { get; set; }
        
        // Banka transferi ise referans numarası
        public string BankTransferReference { get; set; }
        
        // Hangi dağıtım dönemine ait (aylık/yıllık raporlama için)
        public DateTime DistributionPeriodStart { get; set; }
        public DateTime DistributionPeriodEnd { get; set; }
        
        // İlişkili dağıtım kaydı ID
        public int? DistributionId { get; set; }
        
        // Belge numarası
        public string DocumentNumber { get; set; }
        
        // İlgili dokümanlar listesi
        public List<TransactionDocument> Documents { get; set; } = new List<TransactionDocument>();
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        // İşlem durumu
        public bool IsActive { get; set; }
        
        // Onay durumu
        public bool IsApproved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovalDate { get; set; }
        
        // İptal edilme durumu
        public bool IsCancelled { get; set; }
        public string CancelledBy { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string CancellationReason { get; set; }

        public RevenueTransaction()
        {
            TransactionDate = DateTime.Now;
            PaymentStatus = PaymentStatus.Beklemede;
            IsActive = true;
            Currency = "TRY";
        }
    }
    
    // İşlem dokümanları
    public class TransactionDocument
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // İlişkili işlem ID
        public int TransactionId { get; set; }
        
        // Doküman tipi
        public RevenueDocumentType DocumentType { get; set; }
        
        // Dosya adı
        public string FileName { get; set; }
        
        // Dosya yolu
        public string FilePath { get; set; }
        
        // Dosya boyutu (byte)
        public long FileSize { get; set; }
        
        // MIME tipi
        public string ContentType { get; set; }
        
        // Açıklama
        public string Description { get; set; }
        
        // Yükleme tarihi
        public DateTime UploadDate { get; set; }
        
        // Yükleyen kullanıcı
        public string UploadedBy { get; set; }
    }
    
    // Gelir işlem tipi
    public enum RevenueTransactionType
    {
        // Kira geliri
        Rent = 0,
        
        // Ortak alan geliri
        CommonAreaRevenue = 1,
        
        // Otopark geliri
        ParkingRevenue = 2,
        
        // Etkinlik geliri
        EventRevenue = 3,
        
        // Diğer gelirler
        OtherRevenue = 4
    }
    
    // Doküman tipi
    public enum RevenueDocumentType
    {
        // Sözleşme
        Contract = 0,
        
        // Fatura
        Invoice = 1,
        
        // Makbuz
        Receipt = 2,
        
        // Dekont
        BankStatement = 3,
        
        // Diğer
        Other = 4
    }
} 