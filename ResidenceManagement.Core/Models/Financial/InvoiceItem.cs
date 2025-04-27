using System;
using ResidenceManagement.Core.Models.Financial;

namespace ResidenceManagement.Core.Models.Financial
{
    // Fatura içerisindeki her bir kalemi temsil eden sınıf
    public class InvoiceItem
    {
        // Benzersiz kimlik
        public int Id { get; set; }
        
        // Multi-tenant yapı için firma ve şube bilgileri
        public int FirmaId { get; set; }
        public int SubeId { get; set; }
        
        // Bağlı olduğu fatura
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        
        // Fatura kalem sırası
        public int LineNumber { get; set; }
        
        // Ürün/hizmet adı
        public string ItemName { get; set; }
        
        // Ürün/hizmet kodu (varsa)
        public string ItemCode { get; set; }
        
        // Açıklama
        public string Description { get; set; }
        
        // Miktar
        public decimal Quantity { get; set; }
        
        // Birim (adet, saat, m2, vb.)
        public string Unit { get; set; }
        
        // Birim fiyat (KDV hariç)
        public decimal UnitPrice { get; set; }
        
        // Ara toplam (miktar * birim fiyat)
        public decimal SubTotal { get; set; }
        
        // KDV oranı (%)
        public decimal VATRate { get; set; }
        
        // KDV tutarı
        public decimal VATAmount { get; set; }
        
        // İndirim oranı (%)
        public decimal DiscountRate { get; set; }
        
        // İndirim tutarı
        public decimal DiscountAmount { get; set; }
        
        // Toplam tutar (KDV dahil, indirim düşülmüş)
        public decimal TotalAmount { get; set; }
        
        // İlişkili gider kaydı (varsa)
        public int? ExpenseId { get; set; }
        public Expense Expense { get; set; }
        
        // Aktif/Pasif durumu
        public bool IsActive { get; set; }
        
        // Oluşturma ve güncelleme bilgileri
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        
        public InvoiceItem()
        {
            LineNumber = 1;
            Quantity = 1;
            VATRate = 18; // Varsayılan KDV oranı %18
            DiscountRate = 0;
            IsActive = true;
            CreatedDate = DateTime.Now;
            
            // Hesaplamaları varsayılan değerlerle yap
            CalculateAmounts();
        }
        
        // Tüm tutarları hesapla
        public void CalculateAmounts()
        {
            // Ara toplam hesapla
            SubTotal = Quantity * UnitPrice;
            
            // İndirim tutarını hesapla
            DiscountAmount = SubTotal * (DiscountRate / 100);
            
            // İndirimli ara toplam
            decimal discountedSubTotal = SubTotal - DiscountAmount;
            
            // KDV tutarını hesapla
            VATAmount = discountedSubTotal * (VATRate / 100);
            
            // Toplam tutarı hesapla (KDV dahil)
            TotalAmount = discountedSubTotal + VATAmount;
        }
    }
} 