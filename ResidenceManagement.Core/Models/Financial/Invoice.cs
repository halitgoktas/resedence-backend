using System;
using System.Collections.Generic;
using ResidenceManagement.Core.Entities;
using ResidenceManagement.Core.Entities.Identity;

namespace ResidenceManagement.Core.Models.Financial
{
    // Apartman sakini veya site sakinlerine kesilen faturaları temsil eden sınıf
    public class Invoice
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Fatura numarası
        public string InvoiceNumber { get; set; }
        
        // Fatura türü
        public InvoiceType InvoiceType { get; set; }
        
        // Fatura tarihi
        public DateTime InvoiceDate { get; set; }
        
        // Son ödeme tarihi
        public DateTime DueDate { get; set; }
        
        // İlgili daire
        public int? ApartmentId { get; set; }
        public Apartment Apartment { get; set; }
        
        // İlgili blok/bina (eğer tüm blok için fatura kesiliyorsa)
        public int? BlockId { get; set; }
        public Block Block { get; set; }
        
        // Faturayı kesen kişi/kurum
        public string IssuedBy { get; set; }
        
        // Fatura alıcısı (gerçek kişi veya tüzel kişilik)
        public int? PersonId { get; set; }
        public ResidenceManagement.Core.Entities.Identity.User Person { get; set; }
        
        // Fatura başlığı
        public string Title { get; set; }
        
        // Fatura açıklaması
        public string Description { get; set; }
        
        // Fatura kalemleri
        public ICollection<InvoiceItem> Items { get; set; }
        
        // Ara toplam (KDV hariç)
        public decimal SubTotal { get; set; }
        
        // Toplam KDV tutarı
        public decimal TotalVAT { get; set; }
        
        // İndirim tutarı
        public decimal DiscountAmount { get; set; }
        
        // Toplam tutar (KDV dahil)
        public decimal TotalAmount { get; set; }
        
        // Ödenen tutar
        public decimal PaidAmount { get; set; }
        
        // Kalan tutar
        public decimal RemainingAmount { get; set; }
        
        // Ödeme durumu
        public InvoicePaymentStatus PaymentStatus { get; set; }
        
        // Son ödeme tarihi
        public DateTime? LastPaymentDate { get; set; }
        
        // Gecikme faizi oranı (%)
        public decimal LatePaymentInterestRate { get; set; }
        
        // Gecikme faizi tutarı
        public decimal LatePaymentInterestAmount { get; set; }
        
        // Faturayla ilişkili ödemeler
        public ICollection<Payment> Payments { get; set; }
        
        // Faturanın iptal edilip edilmediği
        public bool IsCancelled { get; set; }
        
        // İptal tarihi
        public DateTime? CancellationDate { get; set; }
        
        // İptal nedeni
        public string CancellationReason { get; set; }
        
        // Fatura durumu
        public InvoiceStatus Status { get; set; }
        
        // E-Fatura mı?
        public bool IsElectronicInvoice { get; set; }
        
        // E-Fatura/E-Arşiv numarası
        public string ElectronicInvoiceNumber { get; set; }
        
        // Fatura belgesi dosya yolu
        public string DocumentPath { get; set; }
        
        // Faturayla ilgili notlar
        public string Notes { get; set; }
        
        // İlişkili gider dağıtımı (eğer bir gider dağıtımından oluşturulduysa)
        public int? ExpenseDistributionId { get; set; }
        public ExpenseDistribution ExpenseDistribution { get; set; }
        
        // Aktif/Pasif durumu
        public bool IsActive { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        public Invoice()
        {
            Items = new List<InvoiceItem>();
            Payments = new List<Payment>();
            InvoiceDate = DateTime.Now;
            Status = InvoiceStatus.Draft;
            PaymentStatus = InvoicePaymentStatus.Unpaid;
            IsActive = true;
            IsCancelled = false;
            CreatedDate = DateTime.Now;
        }
    }
    
    // Fatura türleri
    public enum InvoiceType
    {
        // Aidat faturası
        Dues = 0,
        
        // Elektrik faturası
        Electricity = 1,
        
        // Su faturası
        Water = 2,
        
        // Doğalgaz faturası
        NaturalGas = 3,
        
        // İnternet faturası
        Internet = 4,
        
        // Bakım faturası
        Maintenance = 5,
        
        // Onarım faturası
        Repair = 6,
        
        // Güvenlik hizmeti faturası
        Security = 7,
        
        // Temizlik hizmeti faturası
        Cleaning = 8,
        
        // Diğer faturalar
        Other = 9
    }
    
    // Fatura durumu
    public enum InvoiceStatus
    {
        // Taslak
        Draft = 0,
        
        // Yayınlandı
        Published = 1,
        
        // Gönderildi
        Sent = 2,
        
        // Ödendi
        Paid = 3,
        
        // Kısmen ödendi
        PartiallyPaid = 4,
        
        // Gecikti
        Overdue = 5,
        
        // İptal edildi
        Cancelled = 6
    }
    
    // Ödeme durumu
    public enum InvoicePaymentStatus
    {
        // Ödenmedi
        Unpaid = 0,
        
        // Kısmen ödendi
        PartiallyPaid = 1,
        
        // Tamamen ödendi
        Paid = 2,
        
        // Gecikti
        Overdue = 3,
        
        // İade edildi
        Refunded = 4,
        
        // İptal edildi
        Cancelled = 5
    }
} 