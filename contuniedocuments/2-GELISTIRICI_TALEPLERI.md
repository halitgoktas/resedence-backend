# Rezidans ve Site Yönetim Sistemi - Geliştirici Talepleri

Bu belge, Rezidans ve Site Yönetim Sistemi projesi için istenen ek özellikleri ve iyileştirmeleri içermektedir.

## Çoklu Döviz ve Dil Desteği Talepleri

1. **Çoklu Döviz Desteği**
   - Ana para birimi TL olarak ayarlanabilmeli ve parametrik olarak değiştirilebilmeli
   - TCMB'den otomatik kur çekme ve zamanlayıcısı oluşturulmalı
   - Kurlar günlük olarak otomatik güncellenebilmeli
   - Her firma/site için ana para birimi farklı olabilmeli (TL, EUR, USD, GBP vb.)
   - Ödemeler farklı para birimlerinde alınabilmeli ve ana para birimine otomatik çevrilmeli
   - Raporlar ve görünümler ana para birimi ve diğer para birimleri cinsinden görüntülenebilmeli

2. **Çoklu Dil Desteği**
   - Varsayılan olarak Türkçe ve İngilizce dilleri
   - Ek olarak Almanca, Rusça, Arapça ve Farsça dil desteği eklenmeli
   - Kullanıcı bazlı dil tercihi saklanabilmeli
   - Dil değiştirme özelliği tüm ekranlarda kolay erişilebilir olmalı
   - Tarih, saat ve para birimi formatları seçilen dile göre otomatik düzenlenmeli

## Rezervasyon ve Müsaitlik Yönetimi Talepleri

1. **Kiralamaya Uygun Dairelerin Yönetimi**
   - Dairelerin doluluk/boşluk takibi
   - Tarih aralığı bazında müsaitlik takibi
   - Aylık doluluk oranları raporlaması (liste ve çubuk grafik şeklinde)
   - Dairelerin kiralanabilirlik durumunun değiştirilebilmesi
   - Otomatik müsaitlik hesaplaması

2. **Rezervasyon Sistemi**
   - Saatlik, günlük, haftalık, aylık rezervasyon imkanı
   - Rezervasyon çakışmalarını önleyen kontrol sistemi
   - Rezervasyon iptal/değişiklik yönetimi
   - Check-in ve check-out işlemleri
   - Müşteri profil yönetimi

3. **Ücretli ve Ücretsiz Aktivite Alanları Yönetimi**
   - Aktivite alanlarının (havuz, spor salonu, toplantı salonu vb.) rezervasyonu
   - Saatlik müsaitlik takibi
   - Ücretli aktivitelerin fiyatlandırılması
   - Rezervasyon onay/red süreci
   - Aktivite alanı doluluk raporları

4. **Teknik Servis ve Hizmet Planlaması**
   - Dairelere verilecek teknik servis hizmetlerinin planlanması
   - Hizmetlerin ücretlendirilmesi
   - Ücret tarifesi ve faturalandırma
   - Hizmetlerin daire hesabına otomatik yansıtılması
   - Saatlik hizmet rezervasyonu ve takibi
   - Hizmet geçmişinin görüntülenmesi

5. **İşlem ve Hizmet Takibi**
   - Hangi işlemin kim tarafından yapıldığı
   - İşlem sonucu ve geliri
   - Hangi hizmetin hangi daireye verildiği
   - Hizmet geçmişi ve detayları

## Finansal Takip ve Raporlama Talepleri

1. **Gelir-Gider Yönetimi**
   - Kiralanan dairelerden elde edilen gelirin takibi
   - Gelirin ev sahibi ve site yönetimi arasında dağılımı (% veya sabit oran)
   - Gelir türlerine göre sınıflandırma ve raporlama
   - Giderlerin türlerine göre takibi ve analizi
   - Grafiksel görünümler ve özet/detay raporlar

2. **Aidat Yönetimi**
   - Daire sahiplerinin aidat borçlarını mahsuplaştırma ekranı
   - Tekil veya toplu dönemsel mahsuplaştırma
   - Aylık veya yıllık periyotlarla otomatik mahsuplaştırma
   - Mükerrer mahsuplaştırmayı önleyici kontroller
   - Mahsuplaştırma geçmişi ve raporları

3. **Tahsilat Sistemi**
   - Tahsilat şekli seçimi (banka, kredi kartı, nakit, çek vb.)
   - Tahsilat tarihi, saati ve yapan kişi bilgileri
   - Farklı para birimlerinde ödeme kabul etme
   - Kurlar arası otomatik dönüşüm
   - Tahsilat makbuzu ve dekont oluşturma

4. **Finansal Raporlar**
   - Aylık ve yıllık bazda gelir-gider raporları
   - Tahsilat durum raporları
   - Borç-alacak dengesi raporları
   - Daire bazında aidat ödeme performansı
   - Site giderlerinin detaylı dökümü
   - Grafiksel gösterimler (pasta, çubuk, çizgi grafikleri)

## KBS (Kimlik Bildirimi Sistemi) Entegrasyonu

1. **Kimlik Bildirimi Yönetimi**
   - Bir rezervasyona birden fazla aile üyesi kimlik bilgisi bağlanabilmesi
   - Jandarma KBS raporunun otomatik hazırlanması ve gönderilmesi
   - Site sakinleri için kimlik bildirim sistemi
   - Dairede kalan kişilerin toplu görüntülenmesi
   - Kimlik bilgilerinin ayrı ayrı girilebilmesi

2. **Giriş-Çıkış Bildirimleri**
   - Giriş işlemleri ve bildirimleri
   - Çıkış işlemleri ve bildirimleri
   - Gönderilen ve gönderilmeyen bildirimlerin takibi
   - Gönderim tarihi ve saati kaydı
   - Otomatik gönderim seçeneği (parametrik)

## Sistem Geliştirme ve Optimizasyon Talepleri

1. **Veritabanı ve Model Geliştirmeleri**
   - Tüm modeller için validasyon kontrollerinin eklenmesi
   - Entity Framework için migration dosyalarının tamamlanması
   - İlişkisel bağlantıların kontrol edilmesi ve optimizasyonu
   - Veritabanı indekslerinin oluşturulması
   - Performans iyileştirmeleri

2. **Güvenlik ve Loglama**
   - Tüm işlemlerin detaylı loglanması
   - Kullanıcı bazlı yetkilendirme sisteminin geliştirilmesi
   - İşlem bazlı yetkilendirme kontrolleri
   - Güvenlik denetimi ve penetrasyon testleri
   - GDPR ve KVKK uyumluluğu

3. **Arayüz Geliştirmeleri**
   - Responsive tasarımın tamamlanması
   - Mobil uyumlu arayüz
   - Kullanıcı deneyiminin iyileştirilmesi
   - Grafik ve görselleştirme ögelerinin artırılması
   - Hızlı erişim menüleri ve dashboard özelleştirmeleri

4. **Entegrasyonlar**
   - Ödeme sistemleri entegrasyonu (banka, kredi kartı vb.)
   - E-Fatura ve E-Arşiv entegrasyonu
   - SMS ve e-posta bildirim sistemleri
   - Mobil uygulama entegrasyonu
   - IoT cihazları entegrasyonu (RFID, plaka tanıma vb.) 