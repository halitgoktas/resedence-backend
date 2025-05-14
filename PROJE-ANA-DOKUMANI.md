# MEKİK RESIDENCE MANAGER PROJESİ

## 1. GENEL BAKIŞ

### 1.1. Vizyon ve Amaç

Mekik Residence Manager, modern konut kompleksleri, rezidanslar ve toplu yaşam alanlarının yönetimini dijitalleştiren, kolaylaştıran ve optimize eden kapsamlı bir yazılım çözümüdür. Bu sistem, site yöneticileri, rezidans sakinleri ve yönetim kurulları arasında etkili bir köprü kurarak, tüm tarafların ihtiyaçlarını karşılamayı amaçlamaktadır.

#### Temel Amaçlar
- **Yönetim Süreçlerinin Dijitalleştirilmesi**: Kağıt bazlı işlemlerin ve manuel süreçlerin dijital ortama aktarılması
- **Verimlilik Artışı**: Site yönetiminde zaman ve kaynak tasarrufu sağlayacak optimizasyonlar
- **Şeffaflık ve Hesap Verebilirlik**: Tüm finansal işlemlerin açık ve izlenebilir olması
- **İletişim Etkinliği**: Site sakinleri ve yönetim arasında hızlı ve etkili iletişim kanalları
- **Güvenlik**: Site erişiminin kontrollü ve izlenebilir hale getirilmesi
- **Multi-tenant Yapı**: Birden fazla firmanın ve her firmanın birden fazla şubesinin aynı sistem üzerinden hizmet verebilmesi

### 1.2. Mimari Yapı ve Teknolojiler

#### Genel Mimari
- **Backend**: .NET 8 Web API, N-tier Architecture
- **Frontend**: React.js, Material UI
- **Mobil**: React Native (planlama aşamasında)
- **Veritabanı**: SQL Server

#### Backend Teknolojileri
- **.NET 8.0**: Modern, güvenli ve performanslı framework
- **Entity Framework Core**: ORM aracı
- **JWT Authentication**: Güvenli kimlik doğrulama
- **RESTful API**: Web servisleri için standart mimari
- **Swagger/OpenAPI**: API dokümantasyonu
- **AutoMapper**: Nesne eşleştirme
- **FluentValidation**: Veri doğrulama

#### Frontend Teknolojileri
- **React.js**: UI kütüphanesi
- **Material UI**: Komponent kütüphanesi
- **Context API**: Durum yönetimi
- **React Router**: Sayfa yönlendirme
- **i18next**: Çoklu dil desteği

## 2. PROJE GELİŞTİRME PLANI VE DURUMU

### 2.1. Geliştirme Fazları

#### Faz 1: Temel Altyapı ve Backend ✅ (Tamamlandı)
- [x] Migration'ların ve Seed Data'nın Oluşturulması
- [x] İki Faktörlü Kimlik Doğrulama ve API Versiyonlama

#### Faz 2: Backend Tamamlama 🔶 (Kısmen Tamamlandı)
- [x] Migration'ların ve Seed Data'nın Oluşturulması
- [x] İki Faktörlü Kimlik Doğrulama ve API Versiyonlama
- [x] Çoklu Döviz ve Dil Desteği İyileştirmeleri
- [x] KBS ve Diğer Entegrasyonlar
- [x] Eksik Backend Testlerinin Geliştirilmesi
- [x] Backend Optimizasyonları
- [x] Model Validasyon ve Doğrulama Mekanizmaları

#### Faz 3: Frontend Geliştirme 🔶 (Kısmen Başlandı)
- [x] Temel Frontend Yapısının Tamamlanması
- [x] Ortak Bileşenlerin Geliştirilmesi
- [ ] Responsive Tasarımın Tamamlanması
- [ ] Temel Sayfaların Geliştirilmesi (Kimlik Doğrulama, Daire ve Sakin Yönetimi)
- [ ] Finansal ve Rezervasyon Sayfalarının Geliştirilmesi
- [ ] Dashboard ve Raporlama Ekranlarının Geliştirilmesi
- [ ] Frontend Testlerinin Yazılması

#### Faz 4: Test, Optimizasyon ve Canlıya Alma ❌ (Beklemede)
- [ ] Entegrasyon ve Uçtan Uca Testler
- [ ] Performans Optimizasyonları
- [ ] Güvenlik Denetimi
- [ ] Dokümantasyon ve Eğitim Materyallerinin Hazırlanması
- [ ] CI/CD Pipeline Kurulumu
- [ ] Canlı Ortam Hazırlığı ve Pilot Kullanım
- [ ] Tam Geçiş

#### Faz 5: Mobil Uygulama ve İleriki Geliştirmeler ❌ (Beklemede)
- [ ] Mobil Uygulama Geliştirme
- [ ] İleri Düzey Analitik Özellikleri
- [ ] Üçüncü Parti Entegrasyonların Genişletilmesi
- [ ] Mikroservis Mimarisine Geçiş
- [ ] Yapay Zeka ve Makine Öğrenmesi Entegrasyonları

### 2.2. Öncelikli Görevler

#### Backend Öncelikleri
1. Para birimi ve sayısal değerler için validasyon kuralları
2. Tarih aralıkları ve iş mantığına uygun validasyonlar
3. Validasyon hatalarının standart formatta döndürülmesi
4. Sorgu optimizasyon ve iyileştirmeleri
5. Rezervasyon sistemi iyileştirmeleri
6. Finansal entegrasyonlar

#### Frontend Öncelikleri
1. Responsive tasarımın tamamlanması
2. Dil değiştirici bileşenin geliştirilmesi
3. Kullanıcı dil tercihi saklama sistemi
4. Daire ve sakin yönetimi ekranları
5. Finansal yönetim ekranları
6. Dashboard ve raporlama ekranları

## 3. SİSTEM MODÜLLERİ VE ÖZELLİKLERİ

### 3.1. Bina ve Daire Yönetimi

#### Site ve Bina Bilgileri
- Site/rezidans temel bilgilerinin yönetimi
- Bina sayısı, kat sayısı, daire sayısı gibi temel verilerin kaydı
- Site özellikleri (havuz, spor salonu, sosyal tesisler vb.)
- Bina teknik özellikleri ve bakım programları
- Bina ortak alanlarının envanteri

#### Daire Yönetimi
- Daire tiplerine göre kategorizasyon (1+1, 2+1, 3+1 vb.)
- Daire metrekaresi, balkon sayısı, banyo sayısı gibi detaylı bilgiler
- Daire durumu takibi (boş, dolu, kiracılı, satılık, kiralık)
- Dairelere ait özel alanların (depo, otopark yeri) yönetimi
- Daire aidat katsayılarının belirlenmesi ve takibi

### 3.2. Sakin ve Malik Yönetimi

#### Daire Sahibi/Malik Yönetimi
- Daire sahiplerinin kişisel bilgilerinin yönetimi
- Mülkiyet oranları ve hisse bilgilerinin takibi
- Mülkiyet tarihçesi ve değişikliklerinin kaydı
- Malik ile iletişim tercihlerinin belirlenmesi
- Uzaktan erişim imkanı ile malik portalı

#### Kiracı ve Sakin Yönetimi
- Kiracı bilgilerinin detaylı kaydı ve yönetimi
- Kira sözleşmesi bilgilerinin takibi
- Kiracı değişim süreçlerinin yönetimi
- Aile üyeleri ve birlikte yaşayan kişilerin kaydı
- Oturan profili oluşturma ve takip etme

#### KBS (Kimlik Bildirim Sistemi) Entegrasyonu
- Sakinlerin kimlik bilgilerinin resmi olarak bildirilmesi
- Yasal zorunlulukların otomatik olarak karşılanması
- Kimlik bilgilerinin güvenli şekilde saklanması
- Bildirim raporlarının oluşturulması
- Devlet entegrasyonlarının sağlanması

### 3.3. Finansal Yönetim

#### Aidat Yönetimi
- Aidat tutarlarının daire bazında belirlenmesi
- Aidat hesaplamalarında katsayı sisteminin kullanılması
- Toplu aidat oluşturma ve takip etme
- Geçmiş dönem aidatlarının arşivlenmesi
- Aidat ödeme takibi ve borç analizi

#### Gider Yönetimi
- Site giderlerinin kategorilere ayrılması
- Fatura girişi ve takibi
- Düzenli ve tek seferlik giderlerin yönetimi
- Gider onay süreçleri ve yetkilendirmeleri
- Gider raporlaması ve bütçe karşılaştırması

#### Bütçe Planlama ve Takibi
- Yıllık ve aylık bütçe oluşturma
- Gerçekleşen giderlerin bütçe ile karşılaştırılması
- Bütçe sapma analizleri
- Gelecek dönem projeksiyonları
- Finansal trend analizi ve raporları

#### Çoklu Para Birimi Desteği
- Farklı para birimlerinde işlem yapabilme
- Otomatik kur güncellemeleri (TCMB entegrasyonu)
- Para birimi çevrimleri ve kur farkı hesaplamaları
- Raporlarda çoklu para birimi desteği
- Güncel ve geçmiş kur değerlerinin saklanması

### 3.4. Teknik Servis ve Bakım Yönetimi

#### Arıza ve Bakım Talepleri
- Sakinlerin online arıza/bakım bildirimi yapabilmesi
- Taleplerin kategorize edilmesi ve önceliklendirilmesi
- Teknik ekibe iş emri oluşturma
- Talep durumunun takibi ve güncellenmesi
- Tamamlanan işlerin raporlanması ve değerlendirilmesi

#### Planlı Bakım Programları
- Periyodik bakım planlarının oluşturulması
- Bakım takviminin yönetimi ve hatırlatmalar
- Bakım görevlerinin tanımlanması ve atanması
- Bakım maliyetlerinin takibi
- Bakım geçmişinin kaydı ve raporlanması

#### Envanter ve Demirbaş Yönetimi
- Site demirbaşlarının kaydı ve takibi
- Demirbaş zimmet işlemleri
- Malzeme ve yedek parça envanteri
- Stok takibi ve sipariş yönetimi
- Demirbaş amortisman hesaplamaları

### 3.5. İletişim ve Bildirim Sistemi

#### Duyuru Yönetimi
- Site genelinde veya belirli gruplara duyuru yayınlama
- Duyuru öncelik seviyeleri ve kategorizasyonu
- Duyuruların geçerlilik sürelerinin belirlenmesi
- Duyuruların okunma durumunun takibi
- Arşivlenen duyuruların yönetimi

#### Şikayet ve Öneri Sistemi
- Sakinlerden şikayet ve önerilerin toplanması
- Şikayetlerin ilgili birimlere yönlendirilmesi
- Çözüm süreçlerinin takibi ve raporlanması
- Şikayetlerin kategorize edilmesi ve analizi
- Çözülen şikayetlerin kapatılması ve memnuniyet ölçümü

#### Çoklu Bildirim Kanalları
- SMS ile bildirim gönderimi
- E-posta bildirimleri
- Uygulama içi anlık bildirimler
- WhatsApp entegrasyonu (isteğe bağlı)
- Bildirim tercihleri ve kişiselleştirme

### 3.6. Rezervasyon ve Etkinlik Yönetimi

#### Ortak Alan Rezervasyonları
- Sosyal tesisler, toplantı salonları, spor alanları için rezervasyon
- Rezervasyon takvimi ve uygunluk kontrolü
- Ücretli/ücretsiz rezervasyon seçenekleri
- Rezervasyon onay süreçleri
- Rezervasyon iptal ve değişiklik yönetimi

#### Etkinlik Yönetimi
- Site içi etkinliklerin planlanması ve duyurulması
- Etkinlik katılım formları ve kayıtları
- Etkinlik bütçeleri ve harcama takibi
- Etkinlik görselleri ve dokümanlarının yönetimi
- Etkinlik değerlendirme anketleri

#### Ziyaretçi Yönetimi
- Ziyaretçi kaydı ve giriş izinleri
- Ziyaretçi bilgilendirme ve karşılama
- Ziyaretçi giriş-çıkış takibi
- Tekrarlayan ziyaretçiler için hızlı kayıt
- Ziyaretçi istatistikleri ve raporları

### 3.7. Güvenlik ve Erişim Kontrolü

#### RFID Kart Yönetimi
- Sakinler ve personel için RFID kart tanımlama
- Kart aktivasyon ve deaktivasyon işlemleri
- Kayıp/çalıntı kart yönetimi
- Kart erişim seviyelerinin belirlenmesi
- Kart kullanım loglarının tutulması

#### Plaka Tanıma Sistemi
- Araç plakalarının kaydı ve yönetimi
- Otomatik plaka tanıma entegrasyonu
- Misafir araç girişlerinin yönetimi
- Otopark kullanım istatistikleri
- Plaka bazlı giriş-çıkış raporları

#### Erişim Logları ve Raporlama
- Tüm giriş-çıkışların kaydedilmesi
- Kişi, araç ve ziyaretçi bazında raporlama
- Erişim anomalilerinin tespiti
- Güvenlik olay raporları
- Geçmiş erişim kayıtlarının arşivlenmesi

### 3.8. Dokümantasyon ve Arşiv Yönetimi

#### Belge Yönetimi
- Site ve bina ile ilgili tüm resmi belgelerin dijital arşivi
- Proje, sözleşme ve yönetim planı dokümantasyonu
- Doküman versiyon kontrolü
- Belgelerin kategorize edilmesi ve etiketlenmesi
- OCR ile belge içeriği arama

#### Sözleşme Takibi
- Tedarikçi sözleşmelerinin yönetimi
- Hizmet sözleşmelerinin takibi
- Sözleşme yenileme hatırlatmaları
- Sözleşme maliyet analizleri
- Sözleşme şartlarının izlenmesi

#### Yasal Dokümantasyon
- Kat mülkiyeti kanunu gereklilikleri
- Karar defteri dijitalleştirme
- Toplantı tutanaklarının yönetimi
- Yasal bildirimlerin arşivlenmesi
- Denetleme raporlarının saklanması

### 3.9. Raporlama ve Analitik

#### Finansal Raporlar
- Gelir-gider raporları
- Aidat tahsilat ve borç raporları
- Bütçe karşılaştırma raporları
- Cari hesap ekstreleri
- Finansal trendler ve grafikler

#### Yönetim Raporları
- Site doluluk oranları
- Arıza ve bakım istatistikleri
- Sakin memnuniyeti ölçümleri
- Personel performans raporları
- KPI takip raporları

#### Özelleştirilebilir Dashboard
- Yönetici rolüne göre özelleştirilmiş kontrol panelleri
- Önemli metriklerin anlık görüntülenmesi
- İnteraktif grafik ve tablolar
- Drill-down analiz imkanı
- Rapor dışa aktarma özellikleri (Excel, PDF)

## 4. TEKNİK ALTYAPI VE VERİTABANI YAPISI

### 4.1. Veritabanı Yapısı

Veritabanı temel olarak iki tür tablodan oluşmaktadır:
1. **Tanım Tabloları (Lookup Tables)**: 
   - Sabit değerleri içeren referans tabloları
   - Diğer tablolarda foreign key olarak kullanılacak
   - Örnek: Kullanıcı tipleri, ülkeler, şehirler, daireler

2. **Hareket Tabloları (Transaction Tables)**:
   - İşlem kayıtlarını tutan tablolar
   - Tanım tablolarından referans alan veriler
   - Örnek: Ödemeler, bakım kayıtları, şikayetler

#### Temel Varlık Sınıfları
- **BaseEntity**: Tüm entity sınıflarının kalıtım aldığı temel sınıf (ID, CreatedDate, ModifiedDate, IsDeleted vb.)
- **BaseLookupEntity**: Tanım tabloları için temel sınıf
- **BaseTransactionEntity**: Hareket tabloları için temel sınıf

#### Multi-tenant Yapı
Sistem, çoklu kiracılığı şunlar aracılığıyla destekler:
- **FirmaID**: Yönetim şirketini tanımlar
- **SubeID**: Şirket içindeki şubeleri tanımlar
- Her işlemde bu iki bilgi mutlaka bulunmalıdır

### 4.2. Backend Mimarisi

#### Katmanlı Mimari
- **Core Layer**: Domain varlıkları, arayüzler ve DTO'lar
- **Infrastructure Layer**: Repository'ler ve servisler
- **Application Layer**: İş mantığı ve kullanım durumları
- **API Layer**: RESTful API ve kontrolcüler

#### API Kontrolcüler
- **AuthController**: Kimlik doğrulama ve yetkilendirme
- **CurrencyController**: Para birimi işlemleri
- **ApartmentController**: Daire yönetimi
- **ResidentController**: Sakin yönetimi
- **PaymentController**: Ödeme işlemleri
- **ReservationController**: Rezervasyon yönetimi
- **KbsIntegrationController**: KBS entegrasyonu
- **ReportController**: Raporlama işlemleri

#### Tasarım Desenleri
- **Repository Pattern**: Veri erişiminin soyutlanması
- **Unit of Work**: İşlem bütünlüğü sağlanması
- **CQRS**: Komut ve sorgu sorumluluklarının ayrılması
- **Mediator Pattern**: Bileşenler arası iletişim

### 4.3. Frontend Mimarisi

#### Bileşen Yapısı
- **Sayfa Bileşenleri**: Ana sayfaları temsil eder
- **Ortak Bileşenler**: Yeniden kullanılabilir UI öğeleri
- **Form Bileşenleri**: Veri giriş ve işleme bileşenleri
- **Tablo Bileşeni**: Veri listeleme, filtreleme ve sıralama
- **Dashboard Bileşenleri**: Grafik ve özet görünümler

#### Durum Yönetimi
- **Context API**: Merkezi durum yönetimi
- **React Hooks**: Bileşen içi durum yönetimi
- **Global State**: Uygulama geneli durum bilgileri

#### Çoklu Dil Desteği
- **i18next**: Çoklu dil kütüphanesi
- **Dil Dosyaları**: Türkçe, İngilizce, Almanca, Rusça, Arapça, Farsça
- **Dil Değiştirici**: Kullanıcıların dil tercihi değiştirme imkanı
- **Formatlar**: Para birimi, tarih ve sayı formatları dile göre otomatik uyarlanır

## 5. ENTEGRASYONLAR

### 5.1. KBS (Kimlik Bildirim Sistemi) Entegrasyonu
- Sakin kimlik bilgileri bildirimi
- Giriş-çıkış bildirimlerinin otomatik gönderimi
- Toplu bildirim yönetimi
- Aile üyeleri için bildirim desteği
- Bildirim durumu takibi

### 5.2. Para Birimi ve Kur Entegrasyonları
- TCMB'den otomatik kur çekme
- Zamanlanmış kur güncellemeleri
- Farklı para birimlerini destekleme
- Kur çevrim işlemleri
- Kur geçmişi takibi

### 5.3. Ödeme Sistemi Entegrasyonları
- Kredi kartı ödeme desteği
- Banka havalesi/EFT ile ödeme
- Çoklu ödeme sağlayıcı entegrasyonu (Iyzico, PayTR, PayU)
- 3D Secure ödeme desteği
- Ödeme durumu takibi ve iptal/iade

### 5.4. SMS ve E-posta Entegrasyonları
- Toplu SMS gönderimi
- E-posta bildirimleri
- Şablon bazlı mesajlaşma
- Gönderim durumu takibi
- Dış servis entegrasyonları

### 5.5. E-Fatura ve E-Arşiv Entegrasyonu
- E-Fatura oluşturma ve gönderme
- E-Arşiv fatura desteği
- Yasal uyumluluk sağlama
- Entegratör hizmetleriyle bağlantı
- Fatura takibi ve raporlama

### 5.6. Güvenlik Sistemleri Entegrasyonu
- RFID kart sistemi entegrasyonu
- Plaka tanıma sistemi entegrasyonu
- Güvenlik kameraları entegrasyonu
- Geçiş kontrol sistemleri
- Ziyaretçi yönetim sistemleri

## 6. GELİŞTİRME STANDARTLARI VE KURALLAR

### 6.1. Genel Kodlama Standartları
- Tüm statik metinler çoklu dil desteğiyle hazırlanacak
- Her değişiklik Changelog'a eklenecek
- 400 satırdan fazla olan dosyalar parçalara ayrılacak
- Versiyon uyumsuzluğu olmayacak
- Proje ana yapısı korunacak

### 6.2. Backend Geliştirme Kuralları
- Repository ve Unit of Work pattern kullanılacak
- Her controller için en az bir servis oluşturulacak
- Tüm API'ler JWT token ile güvenli hale getirilecek
- Validasyon için FluentValidation kullanılacak
- Tüm API'ler Swagger ile dokümante edilecek
- Tüm metotlar için birim testler yazılacak
- Her işlem için detaylı loglama yapılacak

### 6.3. Frontend Geliştirme Kuralları
- Her sayfa için ayrı bir bileşen oluşturulacak
- Ortak kodlar için paylaşılan bileşenler kullanılacak
- Tüm sayfalar responsive olacak
- Tüm metinler i18n ile çoklu dil desteğine sahip olacak
- Form validasyonları için Formik ve Yup kullanılacak
- Her bileşen için test yazılacak
- Performans optimizasyonları (lazy loading, memoization) yapılacak

### 6.4. Çoklu Dil ve Çoklu Para Birimi Kuralları
- Tüm metinler dil dosyalarında tanımlanacak
- Dil dosyaları modüler olarak organize edilecek
- Para birimi formatları dile göre otomatik uyarlanacak
- Tarih ve saat formatları dile göre otomatik uyarlanacak
- Kullanıcı tercihleri saklanacak ve hatırlanacak

### 6.5. Klasör Yapısı
- Backend kodları sadece Backend/ klasöründe
- Frontend kodları sadece Frontend/ klasöründe
- Mobil kodları sadece Mobile/ klasöründe
- Ortak kodlar Shared/ klasöründe
- Dokümantasyon Documents/ klasöründe

## 7. GELİŞTİRİCİ TALEPLERİ VE ÖNCELİKLİ GÖREVLER

### 7.1. Çoklu Döviz ve Dil Desteği İyileştirmeleri
- Ana para birimi TL olarak ayarlanabilmeli ve parametrik olarak değiştirilebilmeli
- TCMB'den otomatik kur çekme ve zamanlayıcısı oluşturulmalı
- Her firma/site için ana para birimi farklı olabilmeli
- Ödemeler farklı para birimlerinde alınabilmeli ve ana para birimine otomatik çevrilmeli
- Dil değiştirme özelliği tüm ekranlarda kolay erişilebilir olmalı

### 7.2. Rezervasyon ve Müsaitlik Yönetimi İyileştirmeleri
- Dairelerin doluluk/boşluk takibi
- Tarih aralığı bazında müsaitlik takibi
- Saatlik, günlük, haftalık, aylık rezervasyon imkanı
- Rezervasyon çakışmalarını önleyen kontrol sistemi
- Aktivite alanlarının (havuz, spor salonu, toplantı salonu vb.) rezervasyonu

### 7.3. Finansal Takip ve Raporlama İyileştirmeleri
- Kiralanan dairelerden elde edilen gelirin takibi
- Gelirin ev sahibi ve site yönetimi arasında dağılımı
- Daire sahiplerinin aidat borçlarını mahsuplaştırma ekranı
- Tahsilat şekli seçimi (banka, kredi kartı, nakit, çek vb.)
- Grafiksel gösterimli finansal raporlar

### 7.4. KBS Entegrasyonu İyileştirmeleri
- Bir rezervasyona birden fazla aile üyesi kimlik bilgisi bağlanabilmesi
- Jandarma KBS raporunun otomatik hazırlanması ve gönderilmesi
- Giriş-çıkış işlemleri ve bildirimleri
- Gönderilen ve gönderilmeyen bildirimlerin takibi

### 7.5. Sistem Geliştirme ve Optimizasyon Talepleri
- Tüm modeller için validasyon kontrollerinin eklenmesi
- İlişkisel bağlantıların kontrol edilmesi ve optimizasyonu
- Tüm işlemlerin detaylı loglanması
- Responsive tasarımın tamamlanması
- Hızlı erişim menüleri ve dashboard özelleştirmeleri

## 8. PROJE YÖNETİMİ

### 8.1. Veritabanı Bağlantı Bilgileri
- Server: www.nsmeticax.com
- Catalog: MekikResidence
- User ID: sa
- Password: Qncmk!,22GB

### 8.2. Dil Desteği
Proje aşağıdaki dilleri destekleyecek şekilde geliştirilecektir:
- Türkçe (Varsayılan)
- İngilizce
- Almanca
- Rusça
- Arapça
- Farsça

### 8.3. İş Takibi ve Zamanlama
- Her sprint iki hafta sürecek
- Her sprint sonunda test ve gözden geçirme yapılacak
- Haftada bir kez durum toplantısı yapılacak
- Önceliklendirme product owner tarafından yapılacak
- Kritik hatalar aynı gün çözülecek

### 8.4. İletişim ve Raporlama
- Günlük durum güncellemeleri gönderilecek
- Haftalık ilerleme raporu hazırlanacak
- Sorunlar ve riskler hemen bildirilecek
- Kod incelemesi (code review) her PR için yapılacak
- Dokümantasyon sürekli güncel tutulacak 