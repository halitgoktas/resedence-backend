# Rezidans ve Site Yönetim Sistemi - Proje Ekleme Özellikleri

// Bu dokümanda, ana proje dokümanları dışında sohbet sayfasından talep edilen ve eklenecek olan özellikler listelenecektir.

## Talep Edilen Özellikler

// Henüz yeni bir özellik talebi alınmamıştır. Yeni özellik talepleri bu başlık altına eklenecektir.

## Tamamlanan Özellikler

// Tamamlanan özellikler bu başlık altında listelenecektir.

### 1. Hizmet Yönetim Sistemi
// ServiceCategory: Elektrik, temizlik, tesisat gibi üst kategorileri ve alt kategorileri tanımlayan sınıf
// ServiceType: Farklı hizmet türlerini, fiyatlandırma yapısını ve çoklu para birimi desteğini içeren sınıf
// Her hizmet için standart fiyat, acil durum ek ücreti, hafta sonu ücreti gibi özellikler tanımlandı
// Dairelere verilecek tüm teknik hizmetler (musluk tamiri, elektrik arızaları, tv inyernet sorunları vb), temizlik hizmetleri (perde, gömlek, çamaşır gibi)
// Hizmetler ücretlendirileceği bir ücrete dayalı farklı türlere göre gruplandırılmış (elektrik, temizlik, tesisat vb) üst alt gruplar ve bunlara bağlı hizmetler
// Hizmetleri kiracı veya daire sakini bir entegre yapıda takip edebileceği sistem
// Hizmetler ana para birimi ve farklı para birimlerinde de fiyatlandırılabilir
// Hizmetleri kiracı ve daire sakinleri talep bulunabilir
// Site yönetimi veya tedarikçi bu hizmetleri talep tarihi saatine göre öncelik ve ücretleri belirleyebilir
// Kredi kartı aracılığı tek kart üzerinden ayrı ayrı olarak hizmet geliri ve tahsilatı olarak takip edilebilmesi
// Aylık veya yıllık sözleşme ve toplu tahakkuk yapılması
// Borçlandırma yanı bu işlemlerin daire sahibi veya kiracı kartında görüntülenebilmesi
// Tahsilat ve borçlanmanın ayrı satırlarda ve bakiye olarak görünmesi

## Geliştirme Aşamasındaki Özellikler

// Aşağıdaki dosyalar geliştirilmiştir:
// - ServiceCategory.cs (+67)
// - ServiceTypes.cs (+122) 
// - DuesSettings.cs (+96)
// - ExpenseCategory.cs (+72)
// - Expense.cs (+142)
// - Employee.cs (+204)
// - EmployeeDepartment.cs (+52)
// - EmployeeSalaryPayment.cs (+157)
// - Contract.cs (+189)
// - ContractPaymentSchedule.cs (+88)
// - FinancialSummary.cs (+187)
// - CHANGELOG.md (+10)

## Reddedilen Özellik Talepleri

// Çeşitli nedenlerle reddedilen özellik talepleri ve gerekçeleri bu başlık altında listelenecektir. 