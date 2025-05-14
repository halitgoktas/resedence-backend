# Proje Yol Haritası, Mevcut Durum ve Gelecek Kapsamı

Bu doküman, Mekik Residence Manager projesinin mevcut geliştirme durumunu, tamamlanmış ve devam eden işleri, gelecek için planlanan özellikleri, geliştirici taleplerini ve eksik kalan önemli geliştirmeleri bir araya getirmektedir.

## 1. Genel Proje Fazları ve Durumu

Proje geliştirme süreci ana fazlar halinde planlanmıştır:

*   **Faz 1: Temel Altyapı ve Backend (Büyük Ölçüde Tamamlandı ✅)**
    *   Migration'ların ve Seed Data'nın Oluşturulması
    *   İki Faktörlü Kimlik Doğrulama ve API Versiyonlama
    *   Çoklu Döviz ve Dil Desteği (Temel Altyapı)
    *   KBS Entegrasyonu (Temel Altyapı)
    *   Model Validasyon ve Doğrulama Mekanizmaları (Temel Seviye)
*   **Faz 2: Backend Tamamlama (Devam Ediyor 🔶)**
    *   Kapsamlı Testler (E2E, Benchmark, Güvenlik, İleri Düzey Validasyon)
    *   Detaylı Sorgu Optimizasyonu ve Veritabanı İyileştirmeleri
    *   Gelişmiş Cache Mekanizmaları
    *   Loglama ve Monitoring İyileştirmeleri
    *   Kapsamlı API ve Kod Dokümantasyonu
    *   CI/CD Pipeline İyileştirmeleri
    *   Aşağıda "Backend Geliştirmeleri ve Talepler" başlığında listelenen ek özellikler ve talepler.
*   **Faz 3: Frontend Geliştirme (Kısmen Başlandı 🔶)**
    *   Temel Frontend Yapısının Tamamlanması
    *   Ortak Bileşenlerin Geliştirilmesi
    *   Responsive Tasarımın Tamamlanması
    *   Temel Sayfaların Geliştirilmesi (Kimlik Doğrulama, bazı yönetim ekranları)
    *   Aşağıda "Frontend Geliştirmeleri ve Talepler" başlığında listelenen birçok sayfa ve özelliğin geliştirilmesi.
    *   Kapsamlı Frontend Testleri
*   **Faz 4: Test, Optimizasyon ve Canlıya Alma (Beklemede ❌)**
    *   Kapsamlı Entegrasyon ve Uçtan Uca Testler
    *   Genel Performans Optimizasyonları
    *   Kapsamlı Güvenlik Denetimi
    *   Son Kullanıcı Dokümantasyonları ve Eğitim Materyalleri
    *   Canlı Ortam Hazırlığı ve Pilot Kullanım
*   **Faz 5: Mobil Uygulama ve İleriki Geliştirmeler (Beklemede ❌)**
    *   Mobil Uygulama Geliştirme (iOS & Android)
    *   Yapay Zeka, Blockchain, IoT Entegrasyonları
    *   Mikroservis Mimarisine Geçiş Değerlendirmesi

## 2. Backend Geliştirmeleri ve Talepler

### 2.1. Mevcut Planda Olup Tamamlanmamış veya İyileştirme Gerektirenler:

*   **Testler:**
    *   [ ] Kapsamlı E2E testlerinin tamamlanması
    *   [ ] Benchmark testleri
    *   [ ] Güvenlik testleri
    *   [ ] İleri düzey validasyon testleri (FluentValidation ile sınır/kenar durumları)
*   **Optimizasyonlar:**
    *   [ ] Detaylı sorgu optimizasyonu (N+1 sorunları, LINQ optimizasyonu)
    *   [ ] Veritabanı indeksleme stratejisinin tamamlanması ve uygulanması
    *   [ ] Gelişmiş Cache implementasyonu (Distributed Cache vb.)
    *   [ ] Asenkron operasyonların gözden geçirilmesi ve iyileştirilmesi
    *   [ ] Tüm işlemlerin detaylı loglanması ve log yönetiminin iyileştirilmesi
    *   [ ] Veritabanı ilişki optimizasyonları
*   **Dokümantasyon:**
    *   [ ] Kapsamlı API dokümantasyonunun (Swagger detayları, örnekler) tamamlanması
    *   [ ] Detaylı kod içi dokümantasyon (XML Comments vb.)
    *   [ ] Güncel veritabanı şeması ve diyagramları
    *   [ ] Kapsamlı geliştirici kılavuzu
    *   [ ] Validasyon kuralları ve hata kodları kataloğu
*   **Entegrasyonlar:**
    *   [ ] Ödeme Sağlayıcı Entegrasyonları: Gerçek ödeme sağlayıcılarının (Iyzico, PayTR, PayU) detaylı implementasyonu.
    *   [ ] Entegrasyon hatalarının yönetimi ve detaylı loglama sistemi.
    *   [ ] Ödeme işlemleri için ayrıntılı loglama ve raporlama.
*   **Raporlama API'leri:**
    *   [ ] Raporlarda filtre ve parametre validasyonu.
*   **Diğer:**
    *   [ ] Mikroservis mimarisine geçiş araştırması ve fizibilitesi.

### 2.2. Geliştirici Talepleri ve "Aklıma Gelenler"den Çıkan Backend İhtiyaçları:

*   **Müşteri (Site/Rezidans Kullanıcısı) Yönetimi:**
    *   [ ] Müşterilerin satın alma tarihi, fatura bilgileri, aylık/yıllık ödeme takibi.
    *   [ ] Proje gelirlerinin müşteri bazlı takibi.
    *   [ ] Müşteri cari hareketlerinin takibi.
    *   [ ] Kullanılan daire/site/rezidans sayısının müşteri bazlı takibi.
    *   [ ] Üyelik yönetimi: Demo hesap oluşturma, kısıtlı erişim, demo'dan gerçeğe geçiş süreçleri.
*   **Gelişmiş Çoklu Döviz Desteği:**
    *   [ ] Ana para biriminin (TL) sistem genelinde parametrik olarak ayarlanabilmesi.
    *   [ ] TCMB'den otomatik kur çekme ve güncelleme zamanlayıcısının stabil çalışması.
    *   [ ] Sistem parametrelerinde "otomatik kur çekme" ayarının kullanıcı paneline eklenmesi.
    *   [ ] Her firma/site için farklı ana para birimi tanımlayabilme (TL, EUR, USD, GBP vb.).
    *   [ ] Tahsilat ve ödemelerde farklı para birimlerinde işlem kabul etme ve ana para birimine otomatik çevrim mekanizmasının iyileştirilmesi.
    *   [ ] Para birimi dönüşümlerinde ondalık hassasiyet ayarlarının yapılandırılabilir olması.
*   **Rezervasyon Sistemi İyileştirmeleri:**
    *   [ ] Saatlik rezervasyon ve müsaitlik takibi.
    *   [ ] Daire doluluk/boşluk takibi ve detaylı raporlama.
    *   [ ] Rezervasyon check-in ve check-out işlemlerinin detaylandırılması.
    *   [ ] Hizmetlerin (teknik servis vb.) fiyatlandırılması ve otomatik olarak daire hesabına yansıtılması.
    *   [ ] Çakışan rezervasyonları kontrol eden kapsamlı validasyon sistemi.
    *   [ ] Rezervasyon için geçerlilik kontrolü (tarih, kapasite vb.).
*   **Finansal Entegrasyonlar ve Takip:**
    *   [ ] Aidat mahsuplaştırma işlemleri için API.
    *   [ ] Kiralanan dairelerden elde edilen gelirin takibi ve ev sahibi/site yönetimi arasında dağıtım mekanizması (% veya sabit oran).
*   **KBS (Kimlik Bildirim Sistemi) Entegrasyonu İyileştirmeleri:**
    *   [ ] Bir rezervasyona birden fazla aile üyesi kimlik bilgisi bağlayabilme.
    *   [ ] Jandarma KBS raporunun otomatik hazırlanması ve gönderilmesi.
    *   [ ] Site sakinleri için kimlik bildirim sistemi (rezervasyon dışı).
    *   [ ] Dairede kalan kişilerin toplu olarak sistemde görüntülenebilmesi.
    *   [ ] Giriş-çıkış bildirimlerinin otomatizasyonu ve detaylı takibi (gönderim tarihi/saati).
    *   [ ] KBS için T.C. Kimlik No validasyonunun güçlendirilmesi.
*   **E-Fatura ve E-Arşiv Entegrasyonu:**
    *   [ ] Kapsamlı E-Fatura ve E-Arşiv entegrasyonunun tamamlanması.
*   **Güvenlik ve Loglama:**
    *   [ ] Tüm önemli işlemlerin detaylı olarak loglanması.
    *   [ ] İşlem bazlı yetkilendirme kontrollerinin detaylandırılması.

## 3. Frontend Geliştirmeleri ve Talepler

### 3.1. Mevcut Planda Olup Tamamlanmamış veya İyileştirme Gerektirenler:

*   **Responsive Tasarım:**
    *   [ ] Tüm ekranların ve bileşenlerin mobil, tablet ve farklı masaüstü çözünürlüklerine tam uyumlu hale getirilmesi.
*   **Ana Sayfalar ve Modüller:**
    *   [ ] Firma ve Şube Tanımları Sayfaları
    *   [ ] Kullanıcı ve Rol Yönetimi Sayfaları
    *   [ ] Daire Yönetimi Sayfaları (Liste, Detay, Ekle/Düzenle, Doluluk/Boşluk Takip)
    *   [ ] Sakin/Kiracı Yönetimi Sayfaları (Liste, Detay, Ekle/Düzenle, KBS Entegrasyon Ekranı, Aile Üyeleri)
    *   [ ] Finansal Sayfalar (Aidat Yönetimi, Ödeme İşlemleri, Gelir-Gider Takibi, Kur Yönetimi, Mahsuplaştırma, Dekont Oluşturma)
    *   [ ] Rezervasyon ve Hizmet Sayfaları (Aktivite Alanları, Saatlik Takvim, Check-in/out, Teknik Servis Planlama)
    *   [ ] Entegrasyon Sayfaları (KBS İşlemleri, RFID Kart, Plaka Tanıma, SMS/E-posta Ayarları)
    *   [ ] Kapsamlı Dashboard ve Raporlama Sayfaları (Yönetim, Kiracı, Mülk Sahibi için ayrı)
*   **Ortak Bileşenler:**
    *   [ ] Gelişmiş Tablo Bileşeni (filtreleme, sıralama, Excel/PDF çıktı)
    *   [ ] Detay Görünümü Bileşeni
    *   [ ] Dashboard Widget'ları
    *   [ ] Bildirim ve Uyarı Bileşenleri
    *   [ ] Grafiksel Görselleştirme Bileşenleri (Chart.js vb.)
*   **Çoklu Dil ve Para Birimi:**
    *   [ ] Kullanıcı dil ve para birimi tercihleri saklama ve otomatik uyarlama sisteminin tamamlanması.
    *   [ ] Almanca ve Farsça dil dosyalarının tamamlanması.
    *   [ ] Gelişmiş dil değiştirici bileşeni (bayrak ve dil adı ile).
    *   [ ] Para birimi seçici ve dönüştürücü bileşeni.
    *   [ ] Finansal ekranlarda çoklu para birimi desteğinin tam entegrasyonu.
*   **Testler:**
    *   [ ] Kapsamlı komponent testleri.
    *   [ ] E2E (uçtan uca) testler.
    *   [ ] Kullanıcı arayüzü ve kullanılabilirlik testleri.
    *   [ ] Çoklu dil ve para birimi fonksiyonları testleri.
*   **Diğer:**
    *   [ ] PWA (Progressive Web App) özellikleri.

### 3.2. Geliştirici Talepleri ve "Aklıma Gelenler"den Çıkan Frontend İhtiyaçları:

*   **Uygulama İçi İletişim Modülü Arayüzleri:**
    *   [ ] Site/rezidans yöneticilerinin geliştirme taleplerini, sorunlarını iletebileceği ve takip edebileceği arayüzler.
*   **Admin Dashboard Genişletmeleri:**
    *   [ ] Müşteri (site/rezidans) yönetimi verilerinin (satın alma, ödeme, demo durumu vb.) admin dashboard'unda gösterimi.
    *   [ ] İletişim modülünden gelen taleplerin/sorunların admin dashboard'unda listelenmesi.
*   **KBS Entegrasyonu Arayüzleri:**
    *   [ ] KBS gönderim takip ve raporlama ekranı.
    *   [ ] Giriş-çıkış bildirim yönetimi arayüzü.
*   **Ödeme Entegrasyon Arayüzleri:**
    *   [ ] Kredi kartı ödeme formu.
    *   [ ] Banka havalesi/EFT bilgileri görüntüleme ekranı.
    *   [ ] Ödeme işlemleri takip sayfası.
    *   [ ] Taksitli ödeme seçenekleri ekranı (eğer backend destekliyorsa).
*   **Finansal Arayüz İyileştirmeleri:**
    *   [ ] Döviz kurları izleme ve güncelleme sayfası.
    *   [ ] Para birimi dönüşüm hesaplama aracı.
    *   [ ] Aidat mahsuplaştırma ekranı.
    *   [ ] Tahsilat şekli ve döviz tipine göre ödeme alma arayüzü.
    *   [ ] Gelir-gider dağılım ve analiz ekranı.
    *   [ ] Tahsilat dekont ve makbuz oluşturma ve görüntüleme.
*   **Raporlama Arayüzleri:**
    *   [ ] Çoklu para birimi raporları ve döviz kurları raporu.
    *   [ ] Doluluk/boşluk ve rezervasyon performans raporları (grafiksel).
    *   [ ] Aylık ve yıllık gelir-gider raporları (detaylı ve grafiksel).
    *   [ ] Gelir türlerine göre dağılım raporları.
    *   [ ] Site giderleri detay dökümü.
    *   [ ] Aidat tahsilat performans raporları.

## 4. Mobil Uygulama Geliştirmeleri (Planlanan ❌)

*   [ ] React Native proje kurulumu.
*   [ ] Backend API entegrasyonu.
*   [ ] Temel Ekranların Geliştirilmesi:
    *   Giriş, Dashboard, Profil
    *   Daire ve Sakin Bilgileri
    *   Aidat Görüntüleme ve Mobil Ödeme
    *   Rezervasyon Oluşturma ve Takip (Aktivite Alanları)
    *   Teknik Servis Talep Oluşturma ve Takip
    *   Duyuru ve Bildirimler
*   [ ] Push notification entegrasyonu.
*   [ ] Mobil-spesifik özellikler (QR kod ile giriş, kamera kullanımı vb.).
*   [ ] Çoklu dil ve para birimi desteğinin mobil uygulamaya uyarlanması.
*   [ ] Offline çalışma modu için temel altyapı.
*   [ ] Kapsamlı testler (komponent, E2E, cihaz uyumluluk).

## 5. Genel Proje ve Altyapı Geliştirmeleri

*   **Gelişmiş Raporlama Sistemi:**
    *   [ ] Özelleştirilebilir raporlar oluşturma arayüzü.
    *   [ ] Daha fazla görsel analiz aracı entegrasyonu.
    *   [ ] Tahminsel raporlama yetenekleri.
*   **Yapay Zeka Entegrasyonu (Gelecek Planı):**
    *   [ ] Aidat tahmin modeli.
    *   [ ] Gider optimizasyonu önerileri.
    *   [ ] Kullanıcı davranış analizi.
    *   [ ] Akıllı bildirim sistemi.
*   **Blockchain Entegrasyonu (Gelecek Planı):**
    *   [ ] Akıllı sözleşmeler (örneğin kira kontratları için).
    *   [ ] Token tabanlı ödeme sistemleri değerlendirmesi.
    *   [ ] Güvenli ve değişmez belge yönetimi.
*   **IoT Entegrasyonu (Gelecek Planı):**
    *   [ ] Akıllı sayaç okuma ve faturalandırma.
    *   [ ] Güvenlik kamera sistemi entegrasyonu ve olay algılama.
    *   [ ] Akıllı bina otomasyonu (enerji yönetimi, erişim kontrolü).
*   **Gelişmiş Güvenlik Önlemleri:**
    *   [ ] Kapsamlı güvenlik denetimi ve penetrasyon testleri.
    *   [ ] Zero Trust Security prensiplerinin değerlendirilmesi.
    *   [ ] Gelişmiş MFA (Çok Faktörlü Kimlik Doğrulama) seçenekleri.
    *   [ ] Veri şifreleme politikalarının gözden geçirilmesi.
*   **DevOps İyileştirmeleri:**
    *   [ ] Kapsamlı CI/CD pipeline'larının tüm platformlar için (Backend, Frontend, Mobil) kurulması ve optimize edilmesi.
    *   [ ] Otomatik testlerin CI/CD süreçlerine tam entegrasyonu.
    *   [ ] Infrastructure as Code (IaC) yaklaşımlarının değerlendirilmesi.
    *   [ ] Gelişmiş monitoring ve alerting araçlarının entegrasyonu.

Bu doküman, projenin mevcut eksiklerini, talep edilen yeni özellikleri ve uzun vadeli hedeflerini bir araya getirerek geliştirme süreçlerine rehberlik etmeyi amaçlamaktadır. Önceliklendirme ve detaylı planlama için bir sonraki adım bu maddelerin değerlendirilmesi olmalıdır. 