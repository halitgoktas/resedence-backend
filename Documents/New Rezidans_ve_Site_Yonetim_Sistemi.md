# Rezidans ve Site Yönetim Sistemi

## 1. Giriş ve Amaç

Rezidans ve Site Yönetim Sistemi, modern konut kompleksleri, rezidanslar ve toplu yaşam alanlarının yönetimini dijitalleştiren, kolaylaştıran ve optimize eden kapsamlı bir yazılım çözümüdür. Bu sistem, site yöneticileri, rezidans sakinleri ve yönetim kurulları arasında etkili bir köprü kurarak, tüm tarafların ihtiyaçlarını karşılamayı amaçlamaktadır.

### 1.1. Temel Amaçlar

- **Yönetim Süreçlerinin Dijitalleştirilmesi**: Kağıt bazlı işlemlerin ve manuel süreçlerin dijital ortama aktarılması
- **Verimlilik Artışı**: Site yönetiminde zaman ve kaynak tasarrufu sağlayacak optimizasyonlar
- **Şeffaflık ve Hesap Verebilirlik**: Tüm finansal işlemlerin açık ve izlenebilir olması
- **İletişim Etkinliği**: Site sakinleri ve yönetim arasında hızlı ve etkili iletişim kanalları
- **Güvenlik**: Site erişiminin kontrollü ve izlenebilir hale getirilmesi
- **Multi-tenant Yapı**: Birden fazla firmanın ve her firmanın birden fazla şubesinin aynı sistem üzerinden hizmet verebilmesi

## 2. Sistem Özellikleri ve Modüller

### 2.1. Bina ve Daire Yönetimi

#### 2.1.1. Site ve Bina Bilgileri
- Site/rezidans temel bilgilerinin (isim, adres, özellikler) yönetimi
- Bina sayısı, kat sayısı, daire sayısı gibi temel verilerin kaydı
- 50'den fazla özellik ile detaylı site tanımlama (havuz, spor salonu, sosyal tesisler vb.)
- Bina teknik özelliklerinin (asansör, jeneratör, ısıtma sistemi) takibi
- Bina ortak alanlarının envanteri ve bakım programları

#### 2.1.2. Daire Yönetimi
- Daire tiplerine göre kategorizasyon (1+1, 2+1, 3+1 vb.)
- Daire metrekaresi, balkon sayısı, banyo sayısı gibi detaylı bilgiler
- Daire durumu takibi (boş, dolu, kiracılı, satılık, kiralık)
- Dairelere ait özel alanların (depo, otopark yeri) yönetimi
- Daire aidat katsayılarının belirlenmesi ve takibi

### 2.2. Sakin ve Malik Yönetimi

#### 2.2.1. Daire Sahibi/Malik Yönetimi
- Daire sahiplerinin kişisel bilgilerinin yönetimi
- Mülkiyet oranları ve hisse bilgilerinin takibi
- Mülkiyet tarihçesi ve değişikliklerinin kaydı
- Malik ile iletişim tercihlerinin belirlenmesi
- Uzaktan erişim imkanı ile malik portalı

#### 2.2.2. Kiracı ve Sakin Yönetimi
- Kiracı bilgilerinin detaylı kaydı ve yönetimi
- Kira sözleşmesi bilgilerinin takibi
- Kiracı değişim süreçlerinin yönetimi
- Aile üyeleri ve birlikte yaşayan kişilerin kaydı
- Oturan profili oluşturma ve takip etme

#### 2.2.3. KBS (Kimlik Bildirim Sistemi) Entegrasyonu
- Sakinlerin kimlik bilgilerinin resmi olarak bildirilmesi
- Yasal zorunlulukların otomatik olarak karşılanması
- Kimlik bilgilerinin güvenli şekilde saklanması
- Bildirim raporlarının oluşturulması
- Devlet entegrasyonlarının sağlanması

### 2.3. Finansal Yönetim

#### 2.3.1. Aidat Yönetimi
- Aidat tutarlarının daire bazında belirlenmesi
- Aidat hesaplamalarında katsayı sisteminin kullanılması
- Toplu aidat oluşturma ve takip etme
- Geçmiş dönem aidatlarının arşivlenmesi
- Aidat ödeme takibi ve borç analizi

#### 2.3.2. Gider Yönetimi
- Site giderlerinin kategorilere ayrılması
- Fatura girişi ve takibi
- Düzenli ve tek seferlik giderlerin yönetimi
- Gider onay süreçleri ve yetkilendirmeleri
- Gider raporlaması ve bütçe karşılaştırması

#### 2.3.3. Bütçe Planlama ve Takibi
- Yıllık ve aylık bütçe oluşturma
- Gerçekleşen giderlerin bütçe ile karşılaştırılması
- Bütçe sapma analizleri
- Gelecek dönem projeksiyonları
- Finansal trend analizi ve raporları

#### 2.3.4. Çoklu Para Birimi Desteği
- Farklı para birimlerinde işlem yapabilme
- Otomatik kur güncellemeleri (TCMB entegrasyonu)
- Para birimi çevrimleri ve kur farkı hesaplamaları
- Raporlarda çoklu para birimi desteği
- Güncel ve geçmiş kur değerlerinin saklanması

### 2.4. Teknik Servis ve Bakım Yönetimi

#### 2.4.1. Arıza ve Bakım Talepleri
- Sakinlerin online arıza/bakım bildirimi yapabilmesi
- Taleplerin kategorize edilmesi ve önceliklendirilmesi
- Teknik ekibe iş emri oluşturma
- Talep durumunun takibi ve güncellenmesi
- Tamamlanan işlerin raporlanması ve değerlendirilmesi

#### 2.4.2. Planlı Bakım Programları
- Periyodik bakım planlarının oluşturulması
- Bakım takviminin yönetimi ve hatırlatmalar
- Bakım görevlerinin tanımlanması ve atanması
- Bakım maliyetlerinin takibi
- Bakım geçmişinin kaydı ve raporlanması

#### 2.4.3. Envanter ve Demirbaş Yönetimi
- Site demirbaşlarının kaydı ve takibi
- Demirbaş zimmet işlemleri
- Malzeme ve yedek parça envanteri
- Stok takibi ve sipariş yönetimi
- Demirbaş amortisman hesaplamaları

### 2.5. İletişim ve Bildirim Sistemi

#### 2.5.1. Duyuru Yönetimi
- Site genelinde veya belirli gruplara duyuru yayınlama
- Duyuru öncelik seviyeleri ve kategorizasyonu
- Duyuruların geçerlilik sürelerinin belirlenmesi
- Duyuruların okunma durumunun takibi
- Arşivlenen duyuruların yönetimi

#### 2.5.2. Şikayet ve Öneri Sistemi
- Sakinlerden şikayet ve önerilerin toplanması
- Şikayetlerin ilgili birimlere yönlendirilmesi
- Çözüm süreçlerinin takibi ve raporlanması
- Şikayetlerin kategorize edilmesi ve analizi
- Çözülen şikayetlerin kapatılması ve memnuniyet ölçümü

#### 2.5.3. Çoklu Bildirim Kanalları
- SMS ile bildirim gönderimi
- E-posta bildirimleri
- Uygulama içi anlık bildirimler
- WhatsApp entegrasyonu (isteğe bağlı)
- Bildirim tercihleri ve kişiselleştirme

### 2.6. Rezervasyon ve Etkinlik Yönetimi

#### 2.6.1. Ortak Alan Rezervasyonları
- Sosyal tesisler, toplantı salonları, spor alanları için rezervasyon
- Rezervasyon takvimi ve uygunluk kontrolü
- Ücretli/ücretsiz rezervasyon seçenekleri
- Rezervasyon onay süreçleri
- Rezervasyon iptal ve değişiklik yönetimi

#### 2.6.2. Etkinlik Yönetimi
- Site içi etkinliklerin planlanması ve duyurulması
- Etkinlik katılım formları ve kayıtları
- Etkinlik bütçeleri ve harcama takibi
- Etkinlik görselleri ve dokümanlarının yönetimi
- Etkinlik değerlendirme anketleri

#### 2.6.3. Ziyaretçi Yönetimi
- Ziyaretçi kaydı ve giriş izinleri
- Ziyaretçi bilgilendirme ve karşılama
- Ziyaretçi giriş-çıkış takibi
- Tekrarlayan ziyaretçiler için hızlı kayıt
- Ziyaretçi istatistikleri ve raporları

### 2.7. Güvenlik ve Erişim Kontrolü

#### 2.7.1. RFID Kart Yönetimi
- Sakinler ve personel için RFID kart tanımlama
- Kart aktivasyon ve deaktivasyon işlemleri
- Kayıp/çalıntı kart yönetimi
- Kart erişim seviyelerinin belirlenmesi
- Kart kullanım loglarının tutulması

#### 2.7.2. Plaka Tanıma Sistemi
- Araç plakalarının kaydı ve yönetimi
- Otomatik plaka tanıma entegrasyonu
- Misafir araç girişlerinin yönetimi
- Otopark kullanım istatistikleri
- Plaka bazlı giriş-çıkış raporları

#### 2.7.3. Erişim Logları ve Raporlama
- Tüm giriş-çıkışların kaydedilmesi
- Kişi, araç ve ziyaretçi bazında raporlama
- Erişim anomalilerinin tespiti
- Güvenlik olay raporları
- Geçmiş erişim kayıtlarının arşivlenmesi

### 2.8. Dokümantasyon ve Arşiv Yönetimi

#### 2.8.1. Belge Yönetimi
- Site ve bina ile ilgili tüm resmi belgelerin dijital arşivi
- Proje, sözleşme ve yönetim planı dokümantasyonu
- Doküman versiyon kontrolü
- Belgelerin kategorize edilmesi ve etiketlenmesi
- OCR ile belge içeriği arama

#### 2.8.2. Sözleşme Takibi
- Tedarikçi sözleşmelerinin yönetimi
- Hizmet sözleşmelerinin takibi
- Sözleşme yenileme hatırlatmaları
- Sözleşme maliyet analizleri
- Sözleşme şartlarının izlenmesi

#### 2.8.3. Yasal Dokümantasyon
- Kat mülkiyeti kanunu gereklilikleri
- Karar defteri dijitalleştirme
- Toplantı tutanaklarının yönetimi
- Yasal bildirimlerin arşivlenmesi
- Denetleme raporlarının saklanması

### 2.9. Raporlama ve Analitik

#### 2.9.1. Finansal Raporlar
- Gelir-gider raporları
- Aidat tahsilat ve borç raporları
- Bütçe karşılaştırma raporları
- Cari hesap ekstreleri
- Finansal trendler ve grafikler

#### 2.9.2. Yönetim Raporları
- Site doluluk oranları
- Arıza ve bakım istatistikleri
- Sakin memnuniyeti ölçümleri
- Personel performans raporları
- KPI takip raporları

#### 2.9.3. Özelleştirilebilir Dashboard
- Yönetici rolüne göre özelleştirilmiş kontrol panelleri
- Önemli metriklerin anlık görüntülenmesi
- İnteraktif grafik ve tablolar
- Drill-down analiz imkanı
- Rapor dışa aktarma özellikleri (Excel, PDF)

## 3. Kullanıcıya Sağlanan Faydalar

### 3.1. Site Yönetimi İçin Faydalar

#### 3.1.1. Operasyonel Verimlilik
- Manuel işlemlerin otomatize edilmesi ile zaman tasarrufu
- Kağıt israfının önlenmesi ve arşivleme kolaylığı
- Tekrarlayan işlemlerin sistem üzerinden yönetimi
- Anlık veri erişimi ile hızlı karar alma
- İş süreçlerinin standardizasyonu

#### 3.1.2. Finansal Kontrol ve Şeffaflık
- Gelir-gider dengesi için anlık takip
- Aidat tahsilat oranlarının artırılması
- Gereksiz harcamaların tespit edilmesi
- Maliyet optimizasyonu için veri analizi
- Finansal raporların otomatik oluşturulması

#### 3.1.3. Yasal Uyumluluk
- KBS (Kimlik Bildirim Sistemi) entegrasyonu
- Yasal süreçlerin otomatik takibi
- Denetim gereksinimlerinin karşılanması
- KVKK uyumlu veri yönetimi
- Yasal bildirimlerin zamanında yapılması

#### 3.1.4. İş Gücü Yönetimi
- Teknik personelin verimli kullanımı
- İş emirlerinin sistematik dağıtımı
- Personel performans takibi
- İnsan kaynakları maliyetlerinin optimizasyonu
- Çalışan memnuniyetinin artırılması

### 3.2. Rezidans Sakinleri İçin Faydalar

#### 3.2.1. Kolay İletişim
- Yönetimle hızlı ve kolay iletişim
- Talep ve şikayetlerin online iletilmesi
- Duyuru ve bildirimlere anlık erişim
- Komşularla güvenli iletişim platformu
- İletişimde kağıt kullanımının azaltılması

#### 3.2.2. Finansal Şeffaflık ve Kolaylık
- Aidat ve fatura detaylarını görüntüleme
- Online ödeme kolaylığı
- Ödeme geçmişine erişim
- Borç durumunu anlık takip etme
- Finansal işlemlerde şeffaflık

#### 3.2.3. Hizmet Kalitesi
- Teknik arızaların hızlı çözümü
- Taleplerin durumunu takip edebilme
- Planlı bakımlar için önceden bilgilendirme
- Etkinlik ve rezervasyonlara kolay erişim
- Yaşam alanı kalitesinin artırılması

#### 3.2.4. Güvenlik ve Konfor
- Güvenli giriş-çıkış kontrolü
- Ziyaretçi yönetimi
- Acil durum bildirimlerine hızlı erişim
- Ortak alanların verimli kullanımı
- Yaşam alanı değerinin korunması

### 3.3. Yönetim Kurulu İçin Faydalar

#### 3.3.1. Stratejik Yönetim
- Veri odaklı karar alma
- Uzun vadeli planlama için analitik araçlar
- Bütçe kontrolü ve finansal analiz
- Performans metriklerinin takibi
- Stratejik hedeflerin ölçümlenmesi

#### 3.3.2. Kaynakların Optimizasyonu
- Bütçe planlaması ve kontrolü
- Tedarikçi maliyetlerinin optimizasyonu
- Enerji ve kaynak kullanımının izlenmesi
- Bakım maliyetlerinin azaltılması
- Personel verimliliğinin artırılması

#### 3.3.3. Risk Yönetimi
- Finansal risklerin erken tespiti
- Güvenlik risklerinin yönetimi
- Yasal uyum risklerinin minimizasyonu
- Bakım ve onarım planlaması ile asset yönetimi
- Kriz durumlarına hızlı müdahale imkanı

## 4. Teknik Özellikler

### 4.1. Mimari Yapı
- Multi-tenant mimarisi ile çoklu firma desteği
- Mikro servis mimarisi ile ölçeklenebilirlik
- Bulut tabanlı veya on-premise çözüm seçenekleri
- %99.9 uptime garantisi
- Felaket kurtarma çözümleri

### 4.2. Güvenlik Özellikleri
- SSL/TLS şifreleme ile güvenli veri transferi
- Çok faktörlü kimlik doğrulama (MFA)
- Role-based access control (RBAC)
- Düzenli güvenlik güncellemeleri
- Veri yedekleme ve kurtarma çözümleri

### 4.3. Entegrasyon Özellikleri
- Açık API desteği
- Banka entegrasyonları
- Online ödeme sistemi entegrasyonları
- Muhasebe yazılım entegrasyonları
- SMS ve e-posta servis entegrasyonları

### 4.4. Mobil Erişim
- iOS ve Android uygulamaları
- Mobil ödeme desteği
- Push bildirimler
- Mobil rezervasyon
- QR kod ile hızlı erişim

## 5. Gelecek Geliştirmeler

### 5.1. Yapay Zeka Entegrasyonu
- Arıza tahmin algoritmaları
- Akıllı enerji yönetimi
- Chatbot ile sakin desteği
- Anomali tespiti
- Talep kategorizasyonu

### 5.2. IoT Entegrasyonu
- Akıllı sayaç entegrasyonu
- Akıllı bina otomasyonu
- Sensör verileri ile optimizasyon
- Uzaktan kontrol imkanları
- Akıllı güvenlik sistemleri

### 5.3. Gelişmiş Analitik
- Öngörüsel bakım planlaması
- Gelir-gider trend analizi
- Sakin davranış analizi
- Kaynak kullanım optimizasyonu
- Gerçek zamanlı iş zekası

## 6. Sonuç

Rezidans ve Site Yönetim Sistemi, modern yaşam alanlarının karmaşık ve çok yönlü yönetim ihtiyaçlarını karşılamak için tasarlanmış kapsamlı bir çözümdür. Sistem, site yöneticilerine operasyonel verimlilik, sakinlere konfor ve kolaylık, yönetim kurullarına ise stratejik karar alma araçları sunarak, tüm paydaşlar için değer yaratmaktadır.

Multi-tenant mimarisi ile farklı site yönetim firmalarının ve çoklu şubelerin tek platform üzerinden hizmet vermesine olanak sağlayan sistem, son teknolojiler ve güvenlik özellikleri ile modern site yönetiminin tüm gereksinimlerini karşılamaktadır.

Rezidans ve Site Yönetim Sistemi, sadece günümüzün ihtiyaçlarını karşılamakla kalmayıp, gelecekteki teknolojik gelişmelere de adapte olabilecek esnek yapısı ile uzun vadeli bir çözüm sunmaktadır. 