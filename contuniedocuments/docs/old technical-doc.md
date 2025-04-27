# Rezidans ve Site Yönetim Sistemi - Teknik Döküman

## 1. Proje Genel Bilgileri

### 1.1. Proje Amacı

Rezidans ve Site Yönetim Sistemi, toplu yaşam alanlarının (rezidanslar, siteler, apartmanlar) yönetimini dijitalleştirmek ve verimli hale getirmek için tasarlanmış kapsamlı bir yazılım çözümüdür. Sistem, otel yönetim yazılımlarına benzer bir rezervasyon mantığıyla çalışan, daire sakinleri ve kiracıların bilgilerini yöneten, aidat takibi yapan, ortak alanların rezervasyonlarını düzenleyen ve site yönetiminin tüm süreçlerini dijitalleştiren bir platformdur.

### 1.2. Hedef Kullanıcılar

- **Site/Rezidans Yöneticileri:** Tüm site operasyonlarını yönetebilecek
- **Daire Sahipleri:** Kendi daireleri ve ödemeleriyle ilgili işlemleri takip edebilecek
- **Kiracılar:** Daire bilgileri, rezervasyonlar ve ödemeler ile ilgili işlemleri gerçekleştirebilecek
- **Site Personeli:** Görevlerine göre sınırlı yetkilerle sistemi kullanabilecek

### 1.3. Kilit Özellikler

- Daire ve sakin yönetimi
- Aidat ve ödeme takibi
- Rezervasyon sistemi
- Kimlik Bildirim Sistemi (KBS) entegrasyonu
- Ücretli ve ücretsiz ortak alan yönetimi
- Personel yönetimi
- Gelir-gider takibi
- Demirbaş yönetimi
- Kapsamlı raporlama ve analiz
- Çoklu dil desteği
- Gelişmiş kullanıcı yetkilendirme sistemi
- Çoklu para birimi desteği

## 2. Teknik Mimari

### 2.1. Backend Mimarisi

Backend, .NET 8 Web API (C#) kullanılarak geliştirilecek ve aşağıdaki katmanlardan oluşacaktır:

- **API Layer:** REST API endpoint'lerini içerir
- **Service Layer:** İş mantığını içerir
- **Repository Layer:** Veri erişim katmanı
- **Domain Layer:** Veri modelleri ve domain logic
- **Infrastructure Layer:** Database, logging, authentication vb. altyapı servisleri

### 2.2. Frontend Mimarisi

Frontend, React.js ve Material UI kullanılarak geliştirilecek ve aşağıdaki yapılardan oluşacaktır:

- **Component Layer:** Yeniden kullanılabilir UI bileşenleri
- **Page Layer:** Sayfa bileşenleri
- **Service Layer:** API çağrıları
- **State Management:** Context API veya gerektiğinde Redux
- **Util Layer:** Yardımcı fonksiyonlar

### 2.3. Mobil Uygulama

Mobil uygulama, React Native ile geliştirilecek ve aşağıdaki yapılardan oluşacaktır:

- **Component Layer:** Yeniden kullanılabilir UI bileşenleri
- **Screen Layer:** Ekran bileşenleri
- **Service Layer:** API çağrıları
- **State Management:** Context API veya gerektiğinde Redux
- **Util Layer:** Yardımcı fonksiyonlar

### 2.4. Veritabanı Mimarisi

- **MSSQL:** Ana veritabanı olarak kullanılacak
- **Entity Framework Core:** ORM aracı olarak kullanılacak
- **Code First Approach:** Migration ve veritabanı şeması için kullanılacak

### 2.5. Entegrasyon Noktaları

- **KBS (Kimlik Bildirim Sistemi):** Jandarma kimlik bildirim sistemi entegrasyonu
- **RFID Kart Sistemi:** Plasis geçiş kontrol sistemi entegrasyonu
- **Ödeme Sistemleri:** Çeşitli ödeme sistemleri entegrasyonu
- **SMS/Email Servisleri:** Bildirim gönderimi için servis entegrasyonu

## 3. Veri Modeli ve İlişkiler

### 3.1. Temel Veri Modelleri

1. **Firma:** Sistemi kullanan firmalar (multi-tenant yapı)
   - FirmaID (Primary Key)
   - FirmaAdi
   - İletişimBilgileri
   - LogoURL
   - AktifMi

2. **Şube:** Firmalara ait şubeler
   - SubeID (Primary Key)
   - FirmaID (Foreign Key)
   - SubeAdi
   - Adres
   - İletişimBilgileri
   - AktifMi

3. **Kullanıcılar:**
   - KullaniciID (Primary Key)
   - FirmaID (Foreign Key)
   - SubeID (Foreign Key)
   - AdSoyad
   - Email
   - Telefon
   - KullaniciTipi (enum: Admin, FirmaYoneticisi, Teknik, Servis, AraEleman, Resepsiyonist, Misafir, Kiracı)
   - SonGirisTarihi
   - AktifMi
   - ErişimSonTarihi

4. **Daireler:**
   - DaireID (Primary Key)
   - FirmaID (Foreign Key)
   - SubeID (Foreign Key)
   - BlokNo
   - KatNo
   - KapiNo
   - DaireTipi
   - OdaSayisi
   - BanyoSayisi
   - BrutAlan
   - NetAlan
   - MulkSahibiID (Foreign Key)
   - KiralikMi
   - SatilikMi
   - OzelOzellikler (JSON)
   - Durum (enum: Boş, Dolu, Bakımda)
   - AidatTutari
   - ParaBirimi

5. **Sakinler:**
   - SakinID (Primary Key)
   - DaireID (Foreign Key)
   - FirmaID (Foreign Key)
   - SubeID (Foreign Key)
   - SakinTipi (enum: MulkSahibi, Kiracı, AileBireyi, Misafir)
   - AdSoyad
   - TCKimlikNo
   - Telefon
   - Email
   - PasaportNo
   - Uyruk
   - GirisTarihi
   - CikisTarihi
   - KullaniciID (Foreign Key)

6. **Rezervasyonlar:**
   - RezervasyonID (Primary Key)
   - DaireID (Foreign Key)
   - FirmaID (Foreign Key)
   - SubeID (Foreign Key)
   - MusteriAdi
   - KisiSayisi
   - GirisTarihi
   - CikisTarihi
   - Fiyat
   - ParaBirimi
   - Durum (enum: Onaylandı, Beklemede, İptal)
   - OdemeID (Foreign Key)

7. **Ödemeler:**
   - OdemeID (Primary Key)
   - FirmaID (Foreign Key)
   - SubeID (Foreign Key)
   - DaireID (Foreign Key)
   - SakinID (Foreign Key)
   - OdemeTipi (enum: Aidat, Kira, HizmetBedeli)
   - Tutar
   - ParaBirimi
   - TLKarsiligi
   - OdemeTarihi
   - SonOdemeTarihi
   - Aciklama
   - OdemeYontemi (enum: Nakit, KrediKarti, Havale)
   - OdemeStatusu (enum: Ödendi, Bekliyor, Gecikti)

8. **AktiviteAlanlari:**
   - AlanID (Primary Key)
   - FirmaID (Foreign Key)
   - SubeID (Foreign Key)
   - AlanAdi
   - Kapasite
   - UcretliMi
   - SaatlikUcret
   - GunlukUcret
   - ParaBirimi
   - Aciklama
   - FotografURL

9. **AlanRezervasyonlari:**
   - RezervasyonID (Primary Key)
   - AlanID (Foreign Key)
   - FirmaID (Foreign Key)
   - SubeID (Foreign Key)
   - SakinID (Foreign Key)
   - BaslangicTarihi
   - BitisTarihi
   - KisiSayisi
   - Tutar
   - ParaBirimi
   - Durum (enum: Onaylandı, Beklemede, İptal)

10. **Hizmetler:**
    - HizmetID (Primary Key)
    - FirmaID (Foreign Key)
    - SubeID (Foreign Key)
    - HizmetAdi
    - Fiyat
    - ParaBirimi
    - Aciklama
    - HizmetTipi (enum: Teknik, Temizlik, Diğer)

11. **HizmetTalepleri:**
    - TalepID (Primary Key)
    - FirmaID (Foreign Key)
    - SubeID (Foreign Key)
    - HizmetID (Foreign Key)
    - SakinID (Foreign Key)
    - DaireID (Foreign Key)
    - TalepTarihi
    - RandevuTarihi
    - Durum (enum: Beklemede, Onaylandı, Tamamlandı, İptal)
    - Notlar

12. **Personel:**
    - PersonelID (Primary Key)
    - FirmaID (Foreign Key)
    - SubeID (Foreign Key)
    - AdSoyad
    - Pozisyon
    - Departman
    - BaslangicTarihi
    - Maas
    - ParaBirimi
    - İletişimBilgileri
    - KullaniciID (Foreign Key)

13. **Demirbaslar:**
    - DemirbasID (Primary Key)
    - FirmaID (Foreign Key)
    - SubeID (Foreign Key)
    - DaireID (Foreign Key, nullable)
    - DemirbasAdi
    - Aciklama
    - AlisTarihi
    - Fiyat
    - ParaBirimi
    - Durum
    - FotografURL

14. **KBS Bildirimleri:**
    - BildirimID (Primary Key)
    - FirmaID (Foreign Key)
    - SubeID (Foreign Key)
    - SakinID (Foreign Key)
    - BildirimTarihi
    - Durum (enum: Başarılı, Hata)
    - HataMesaji
    - SistemKayitNo

15. **GelirGider:**
    - IslemID (Primary Key)
    - FirmaID (Foreign Key)
    - SubeID (Foreign Key)
    - IslemTipi (enum: Gelir, Gider)
    - Kategori
    - Tutar
    - ParaBirimi
    - TLKarsiligi
    - Aciklama
    - IslemTarihi
    - KaynakID (Foreign Key, ilişkili olduğu işlem)
    - KaynakTipi (enum: Aidat, Kira, Hizmet, Diğer)

### 3.2. Önemli İlişkiler

1. **Çoklu Müşteri (Multi-tenant) Yapısı:**
   - Tüm tablolarda FirmaID alanı bulunacak
   - Kullanıcılar sadece kendi firmasına ait verileri görebilecek

2. **Şube Yapısı:**
   - Tüm tablolarda SubeID alanı bulunacak
   - Kullanıcılar yetkilerine göre belirli şubelerdeki verilere erişebilecek

3. **Daire-Sakin İlişkisi:**
   - Bir dairede birden fazla sakin olabilir
   - Sakinlerin daire ile ilişkisi zaman bazlı tutulacak (GirisTarihi, CikisTarihi)

4. **Kullanıcı-Sakin İlişkisi:**
   - Her sakin için bir kullanıcı hesabı oluşturulabilir
   - Kullanıcı hesapları süreli olarak aktif/pasif edilebilir

5. **Rezervasyon-Ödeme İlişkisi:**
   - Her rezervasyon bir veya birden fazla ödeme kaydına sahip olabilir
   - Ödemeler farklı para birimlerinde tutulabilir

## 4. API Yapısı

### 4.1. API Endpoint Yapısı

API'ler aşağıdaki yapıda olacaktır:

```
/api/v1/[resource]/[action]
```

Örnek API endpoint'leri:

```
/api/v1/daireler
/api/v1/daireler/{id}
/api/v1/daireler/filter
/api/v1/sakinler
/api/v1/sakinler/{id}
/api/v1/rezervasyonlar
/api/v1/odemeler
/api/v1/aktivitealanlari
/api/v1/hizmetler
/api/v1/raporlar/daireler
/api/v1/raporlar/finansal
```

### 4.2. API Güvenliği

- **JWT Token Authentication:** Kullanıcı kimlik doğrulaması için
- **Role-Based Authorization:** Rol bazlı yetkilendirme
- **API Rate Limiting:** DDoS koruması için istek sınırlama
- **CORS Politikaları:** Cross-origin request güvenliği

### 4.3. API Versiyonlama

- API'ler `/api/v1/...` formatında versiyonlanacak
- Büyük değişikliklerde yeni versiyon çıkarılacak (v2, v3 vb.)
- Geriye dönük uyumluluk korunacak

## 5. Frontend Uygulama Yapısı

### 5.1. Ana Modüller

1. **Dashboard (Ana Sayfa)**
   - Genel durum özeti
   - Kritik bilgiler
   - Grafikler ve istatistikler

2. **Tanımlar Modülü**
   - Firma tanımları
   - Şube tanımları
   - Kullanıcı tanımları
   - Ayarlar
   - Daire tanımları
   - Aktivite alanları
   - Hizmet tanımları
   - Personel tanımları

3. **Daire Yönetimi**
   - Daire listesi
   - Daire detayı
   - Daire durumu takibi
   - Demirbaş yönetimi

4. **Sakin Yönetimi**
   - Sakin listesi
   - Sakin ekleme/düzenleme
   - Sakin geçmişi
   - KBS işlemleri

5. **Rezervasyon Modülü**
   - Rezervasyon listesi
   - Rezervasyon oluşturma
   - Takvim görünümü
   - Rack ekranı (doluluk durumu)

6. **Finansal Yönetim**
   - Aidat yönetimi
   - Ödeme takibi
   - Gelir-gider takibi
   - Faturalar

7. **Ortak Alan Yönetimi**
   - Ücretli alanlar
   - Ücretsiz alanlar
   - Rezervasyon yönetimi

8. **Personel Yönetimi**
   - Personel listesi
   - Personel görev takibi
   - İzin yönetimi

9. **Raporlama Modülü**
   - Finansal raporlar
   - Doluluk raporları
   - Ödeme raporları
   - Özelleştirilebilir raporlar

10. **Yönetim Modülü**
    - Kullanıcı yönetimi
    - Yetkilendirme
    - Sistem ayarları
    - Log takibi

### 5.2. Sayfalar ve Bileşenler

- Her modül kendi sayfa ve alt sayfalarına sahip olacak
- Ortak bileşenler components/ klasöründe tutulacak
- Modal/dialog yapıları ortak kullanılacak
- Form bileşenleri standart olacak
- Tablo yapıları MUI DataGrid ile oluşturulacak

### 5.3. Tema ve Stil

- Material UI tema yapısı kullanılacak
- Responsive tasarım (mobil uyumlu)
- Dark/light mode desteği
- Özelleştirilebilir renkler ve logolar
- Türkçe metinler ve dil desteği

## 6. Mobil Uygulama Yapısı

### 6.1. Ana Ekranlar

1. **Giriş Ekranı**
   - Kullanıcı girişi
   - Şifremi unuttum

2. **Ana Ekran (Dashboard)**
   - Daire bilgileri
   - Ödeme durumu
   - Duyurular
   - Hızlı işlemler

3. **Rezervasyonlar**
   - Rezervasyon listesi
   - Rezervasyon detayı
   - Rezervasyon oluşturma

4. **Ödemeler**
   - Ödeme geçmişi
   - Ödeme yapma
   - Fatura görüntüleme

5. **Hizmetler**
   - Hizmet talepleri
   - Yeni talep oluşturma
   - Talep takibi

6. **Profil**
   - Kişisel bilgiler
   - Şifre değiştirme
   - Ayarlar

### 6.2. Mobil Özellikleri

- Push notification desteği
- Offline modu
- Kamera entegrasyonu (belge tarama)
- Konum servisleri
- Biyometrik kimlik doğrulama

## 7. Güvenlik ve Yetkilendirme

### 7.1. Kullanıcı Rolleri ve Yetkileri

1. **Admin:** Tüm sisteme tam erişim
2. **Firma Yöneticisi:** Firma verilerine tam erişim
3. **Teknik:** Teknik hizmet ve bakım işlemlerine erişim
4. **Servis:** Servis ve hizmet taleplerine erişim
5. **Ara Eleman:** Sınırlı işlem yetkisi
6. **Resepsiyonist:** Rezervasyon ve giriş-çıkış işlemleri
7. **Misafir (Daire Sahibi):** Kendi daire ve ödeme bilgilerine erişim
8. **Kiracı:** Kendi rezervasyon ve ödeme bilgilerine erişim

### 7.2. Yetki Kontrolleri

- Her form için CRUD (Create, Read, Update, Delete) bazlı yetkilendirme
- Sayfa bazlı erişim kontrolü
- Komponent bazlı görünürlük kontrolü
- API endpoint bazlı yetkilendirme

## 8. Entegrasyonlar

### 8.1. KBS (Kimlik Bildirim Sistemi)

- Sakin bilgilerinin otomatik bildirimi
- Toplu bildirim yapabilme
- Bildirim durumu takibi
- Bildirim logları

### 8.2. RFID Kart Sistemi

- Plasis RFID okuyucu entegrasyonu
- Kart tanımlama ve yönetim
- Giriş-çıkış takibi
- Süreli kart tanımlama

### 8.3. Ödeme Sistemleri

- Banka entegrasyonu
- Kredi kartı ödeme sistemi
- Online ödeme altyapısı
- Otomatik aidat tahsilatı

### 8.4. Bildirim Sistemleri

- E-posta bildirimleri
- SMS bildirimleri
- Push notification (mobil)
- Bildirim şablonları ve özelleştirme

## 9. Raporlama ve Analiz

### 9.1. Standart Raporlar

- Daire doluluk raporu
- Aidat tahsilat raporu
- Gelir-gider raporu
- Rezervasyon raporu
- Personel performans raporu

### 9.2. Özelleştirilebilir Raporlar

- Tarih aralığı seçimi
- Filtreleme seçenekleri
- Gruplamalı raporlar
- Pivot tablolar

### 9.3. Veri Görselleştirme

- Grafikler (çizgi, çubuk, pasta)
- Dashboard widget'ları
- Trend analizleri
- Karşılaştırmalı raporlar

### 9.4. Dışa Aktarma

- Excel formatında dışa aktarma
- PDF formatında dışa aktarma
- Rapor planlama ve otomatik gönderim

## 10. Geliştirme Süreçleri ve Standartları

### 10.1. Geliştirme Sıralaması

1. **Backend Mimarisi**
   - Veritabanı modellemesi
   - Migration scriptleri
   - Repository katmanı
   - Service katmanı
   - API katmanı

2. **Frontend Geliştirme**
   - Bileşen yapısı
   - Sayfa tasarımları
   - API entegrasyonu
   - State yönetimi
   - UI/UX iyileştirmeleri

3. **Mobil Uygulama Geliştirme**
   - Ana ekranlar
   - API entegrasyonu
   - Bildirim sistemi
   - Kullanıcı deneyimi

### 10.2. Test Stratejisi

- Unit testler (Backend ve Frontend)
- Integration testler
- UI testleri
- Performance testleri
- Security testleri

### 10.3. Deployment Stratejisi

- CI/CD pipeline
- Staging ve production ortamları
- Blue/green deployment
- Rollback stratejisi

## 11. Ek Bilgiler

### 11.1. Performans Hedefleri

- Sayfa yüklenme süresi < 2 saniye
- API yanıt süresi < 500ms
- Mobil uygulama başlangıç süresi < 3 saniye
- Eşzamanlı 1000+ kullanıcı desteği

### 11.2. Ölçeklenebilirlik

- Horizontal scaling desteği
- Microservice mimarisine geçiş planı
- Caching stratejisi
- Database sharding planı

### 11.3. Bakım ve Destek

- Otomatik log analizi
- Exception tracking
- Performance monitoring
- Kullanıcı davranış analizi

## 12. Sonuç

Bu teknik döküman, Rezidans ve Site Yönetim Sistemi'nin geliştirme aşamalarını, teknik mimarisini ve işleyişini detaylı bir şekilde açıklamaktadır. Proje, kullanıcı dostu bir arayüz ile kapsamlı bir yönetim çözümü sunarak, site ve rezidans yönetimini dijitalleştirmeyi ve optimize etmeyi hedeflemektedir.

Geliştirme sürecinde, backend, frontend ve mobil uygulama bölümlerinin belirtilen sırayla ve teknik standartlara uygun olarak tamamlanması, sistemin başarılı bir şekilde hayata geçirilmesini sağlayacaktır.
