# Rezidans ve Site Yönetim Sistemi - Geliştirme Yol Haritası

Bu doküman, Rezidans ve Site Yönetim Sistemi'nin geliştirme sürecindeki adımları, kilometre taşlarını ve her bir aşamanın detaylarını içerir. Proje geliştirme sürecinin tüm aşamaları aşağıdaki sırayla gerçekleştirilecektir.

## Faz 1: Backend API ve Veritabanı Geliştirmesi

> **Önkoşul:** Tüm backend mimarisi tamamlanmadan frontend geliştirmesine başlanmayacaktır.

### Sprint 1: Veritabanı Tasarımı ve Temel Mimari (2 hafta)

1. **Hafta 1: Veritabanı Tasarımı**
   - Tüm veri modellerinin tanımlanması
   - İlişkilerin kurulması
   - Multi-tenant yapının oluşturulması (FirmaID alanı)
   - Şube yapısının oluşturulması (FirmaID ve SubeID alanları)
   - Migration scriptlerinin hazırlanması

2. **Hafta 2: Temel Mimari Oluşturma**
   - Repository pattern kurulumu
   - Dependency injection yapılandırması
   - Generic repository ve service katmanı
   - Temel hata yönetimi ve loglama
   - Entity Framework konfigürasyonu

### Sprint 2: Temel API ve Kimlik Doğrulama (2 hafta)

3. **Hafta 3: Kimlik Doğrulama ve Yetkilendirme**
   - JWT tabanlı kimlik doğrulama
   - Role-based yetkilendirme (Admin, Firma Yöneticisi, Teknik, Servis, Ara Eleman, Resepsiyonist, Misafir, Kiracı)
   - Kullanıcı yönetimi API'leri
   - Refresh token mekanizması
   - Erişim süresi kontrolü (kiracı ve daire sahibi için farklı süreler)

4. **Hafta 4: Temel API'ler**
   - Firma ve şube yönetimi API'leri
   - Kullanıcı ve rol yönetimi API'leri
   - Ayarlar API'leri (uygulama parametreleri)
   - Swagger dokümantasyonu

### Sprint 3: Daire ve Sakin Yönetim API'leri (2 hafta)

5. **Hafta 5: Daire Yönetimi API'leri**
   - Daire CRUD işlemleri
   - Daire durumu yönetimi (kiralık/satılık/boş/dolu)
   - Daire detay bilgileri (blok, kapı, kat, tipi, oda sayısı, banyo sayısı, brüt m2)
   - Daire sahibi ilişkileri
   - Demirbaş takibi
   - Daire filtreleme ve arama

6. **Hafta 6: Sakin Yönetimi API'leri**
   - Sakin CRUD işlemleri
   - Otomatik kullanıcı tanımı oluşturma (daire sahibi ve kiracı için)
   - E-posta/SMS bilgilendirme entegrasyonu
   - Sakin-daire ilişkileri
   - Sakin geçmiş kayıtları
   - Daire sahibi değişikliği işlemleri

### Sprint 4: Finansal API'ler ve Aktivite Alanları (2 hafta)

7. **Hafta 7: Finansal API'ler**
   - Aidat yönetimi
   - Ödeme işlemleri
   - Gelir-gider takibi
   - Kira geliri takibi
   - Borçlandırma ve alacaklandırma işlemleri
   - Çoklu para birimi desteği (TL, Euro, USD, GBP)
   - Kur yönetimi

8. **Hafta 8: Aktivite ve Hizmet API'leri**
   - Ücretli aktivite alanları yönetimi
   - Ücretsiz aktivite alanları yönetimi
   - Rezervasyon işlemleri
   - Ücretli hizmetler yönetimi (teknik servis, temizlik vb.)
   - Hizmet talepleri

### Sprint 5: Entegrasyonlar ve Konaklama Yönetimi (2 hafta)

9. **Hafta 9: Entegrasyon API'leri**
   - KBS (Kimlik Bildirim Sistemi) entegrasyonu
   - RFID kart sistemi entegrasyonu
   - Plaka tanımlama entegrasyonu
   - SMS/Email bildirim entegrasyonu

10. **Hafta 10: Konaklama Yönetimi API'leri**
    - Rezervasyon sistemi
    - Giriş-çıkış işlemleri
    - Rack ekranı veri yapısı
    - Personel yönetimi API'leri

### Sprint 6: Raporlama ve Test (2 hafta)

11. **Hafta 11: Raporlama API'leri**
    - Excel/PDF çıktı oluşturma
    - Dashboard veri API'leri
    - Detaylı rapor API'leri
    - Özelleştirilebilir rapor parametreleri

12. **Hafta 12: Test ve Optimizasyon**
    - Unit testler
    - Entegrasyon testleri
    - Performans optimizasyonu
    - Güvenlik testleri

## Faz 2: Frontend Web Uygulaması Geliştirmesi

> **Önkoşul:** Backend API ve veritabanı geliştirmesi tamamlanmış olmalıdır.

### Sprint 7: Temel UI Bileşenleri ve Yapılandırma (2 hafta)

13. **Hafta 13: Temel Yapılandırma ve Layout**
    - Proje yapısı oluşturma
    - Material UI kurulumu
    - Layout ve tema oluşturma
    - Context API yapılandırması
    - Kullanıcı dostu UI tasarım prensiplerinin belirlenmesi

14. **Hafta 14: Ortak Bileşenler**
    - Form bileşenleri
    - Tablo bileşenleri (Excel/PDF çıktı alma özellikli)
    - Detay görüntüleme bileşenleri
    - Bildirim ve alert bileşenleri

### Sprint 8: Kimlik Doğrulama ve Tanımlar Ekranları (2 hafta)

15. **Hafta 15: Kimlik Doğrulama Sayfaları**
    - Giriş sayfası
    - Şifremi unuttum
    - Profil sayfası
    - Yetki kontrolü
    - Erişim süresi yönetimi

16. **Hafta 16: Tanımlar Modülü**
    - Firma ve şube tanımları
    - Kullanıcı tanımları
    - Ayarlar sayfası
    - Rol ve yetki yönetimi

### Sprint 9: Daire ve Sakin Yönetim Sayfaları (2 hafta)

17. **Hafta 17: Daire Yönetim Sayfaları**
    - Daire listesi
    - Daire detay sayfası
    - Daire ekleme/düzenleme formları
    - Demirbaş yönetimi
    - Daire sahibi ilişkilendirme

18. **Hafta 18: Sakin Yönetim Sayfaları**
    - Sakin listesi
    - Sakin detay sayfası
    - Sakin ekleme/düzenleme formları
    - Otomatik kullanıcı oluşturma
    - KBS işlemleri

### Sprint 10: Finansal ve Rezervasyon Sayfaları (2 hafta)

19. **Hafta 19: Finansal Sayfalar**
    - Aidat yönetimi
    - Ödeme işlemleri
    - Gelir-gider takibi
    - Kira geliri takibi
    - Çoklu para birimi gösterimi

20. **Hafta 20: Aktivite ve Hizmet Sayfaları**
    - Ücretli aktivite alanları yönetimi
    - Ücretsiz aktivite alanları yönetimi
    - Rezervasyon oluşturma
    - Hizmet tanımları
    - Hizmet talepleri

### Sprint 11: Konaklama ve Raporlama Sayfaları (2 hafta)

21. **Hafta 21: Konaklama Yönetimi**
    - Rezervasyon sistemi
    - Giriş-çıkış işlemleri
    - Rack ekranı (daire durumları görünümü)
    - Personel yönetimi

22. **Hafta 22: Raporlama Sayfaları**
    - Dashboard görünümü (3 farklı tip: yönetim, kiracı, mülk sahibi)
    - Finansal raporlar
    - Doluluk raporları
    - Excel/PDF çıktı oluşturma

### Sprint 12: Test ve Optimizasyon (2 hafta)

23. **Hafta 23: Frontend Testleri**
    - Komponent testleri
    - Form validasyon testleri
    - End-to-end testler
    - Erişilebilirlik testleri

24. **Hafta 24: Son Kontroller ve Optimizasyon**
    - Performans optimizasyonu
    - Bundle size analizi
    - Kullanıcı deneyimi iyileştirmeleri
    - Browser uyumluluk testleri

## Faz 3: Mobil Uygulama Geliştirmesi

> **Önkoşul:** Web uygulaması tamamen tamamlanmış olmalıdır.

### Sprint 13: Mobil Temel Kurulum ve Ekranlar (2 hafta)

25. **Hafta 25: Mobil Proje Kurulumu**
    - React Native proje oluşturma
    - UI kütüphanesi kurulumu
    - Navigasyon yapılandırması
    - API servis yapılandırması

26. **Hafta 26: Kimlik Doğrulama ve Profil Ekranları**
    - Giriş ekranı
    - Şifremi unuttum
    - Profil ekranı
    - Bildirim yönetimi

### Sprint 14: Temel İşlevler (2 hafta)

27. **Hafta 27: Daire ve Sakin Ekranları**
    - Daire bilgileri
    - Sakin bilgileri
    - Daire durum kontrolü
    - Demirbaş yönetimi

28. **Hafta 28: Finansal Ekranlar**
    - Aidat görüntüleme
    - Ödeme geçmişi
    - Ödeme bildirimleri
    - Fatura görüntüleme

### Sprint 15: Rezervasyon ve Bildirimler (2 hafta)

29. **Hafta 29: Rezervasyon Ekranları**
    - Aktivite alanları listesi
    - Rezervasyon oluşturma
    - Rezervasyon geçmişi
    - Takvim görünümü

30. **Hafta 30: Bildirim ve İletişim**
    - Push notification sistemi
    - Duyuru sistemi
    - Hizmet talepleri
    - İletişim formu

### Sprint 16: Test ve Yayına Alma (2 hafta)

31. **Hafta 31: Mobil Testler**
    - Komponent testleri
    - End-to-end testler
    - Performans testleri
    - Cihaz uyumluluk testleri

32. **Hafta 32: Son Kontroller ve Yayına Alma**
    - Hata düzeltmeleri
    - App Store hazırlığı
    - Google Play Store hazırlığı
    - Kullanım kılavuzu

## Geliştirme Sonrası

33. **Kullanıcı Eğitimi ve Destek (2 hafta)**
    - Yönetici eğitimi
    - Kullanıcı kılavuzu
    - Destek sistemi kurulumu
    - Sık sorulan sorular

34. **Canlı Ortam İzleme ve İyileştirmeler (2 hafta)**
    - Performans izleme
    - Hata izleme ve düzeltme
    - Kullanıcı geri bildirimleri
    - İlk iyileştirme güncellemeleri

## Toplu Zaman Çizelgesi

- **Faz 1: Backend API ve Veritabanı Geliştirmesi** - 12 hafta
- **Faz 2: Frontend Web Uygulaması Geliştirmesi** - 12 hafta
- **Faz 3: Mobil Uygulama Geliştirmesi** - 8 hafta
- **Geliştirme Sonrası** - 4 hafta

**Toplam Proje Süresi:** 36 hafta (yaklaşık 9 ay)

## Önemli Prensipler

1. **Aşamalı İlerleme:** Her faz bir önceki faz tamamlandıktan sonra başlayacaktır.

2. **Sprint Bazlı Geliştirme:** Her sprint 2 haftalık periyotlarla ilerleyecektir.

3. **Yinelemeli Yaklaşım:** Her sprintin sonunda çalışan bir ürün olacaktır.

4. **Sürekli Entegrasyon:** Kod değişiklikleri her gün ana branch'e entegre edilecektir.

5. **Otomatik Testler:** Her yeni özellik için otomatik testler yazılacaktır.

6. **Dokümantasyon:** Tüm API'ler ve önemli fonksiyonlar dokümante edilecektir.

7. **Kod İncelemeleri:** Tüm kod değişiklikleri en az bir başka geliştirici tarafından incelenecektir.

## Risk Planı

1. **Geliştirme Gecikmeleri:**
   - Kritik modüller önceliklendirilerek geliştirme yapılacak
   - İçerik optimize edilerek minimum uygulanabilir ürün (MVP) odaklanılacak

2. **Teknoloji Riskleri:**
   - Başlangıçta proof-of-concept uygulamalar yapılacak
   - Deneyimli geliştiriciler kritik modüllerde çalışacak

3. **Entegrasyon Sorunları:**
   - Erken aşamalarda entegrasyon testleri yapılacak
   - Sahte API'ler (mock) kullanılarak bağımlılıklar azaltılacak

4. **Performans Sorunları:**
   - Her sprintte performans testleri yapılacak
   - Erken aşamalarda optimizasyon yapılacak

## Sonuç

Bu geliştirme yol haritası, Rezidans ve Site Yönetim Sistemi projesinin başarılı bir şekilde tamamlanması için gerekli adımları ve zaman çizelgesini içermektedir. Proje sürecinde, bu yol haritası referans alınarak ilerlenecek ve gerekli durumlarda güncellenecektir.

Proje ekibi, bu yol haritasına uyarak sistematik ve planlı bir şekilde ilerlemelidir. Her aşamanın tamamlanması, bir sonraki aşamaya geçmek için önkoşuldur ve bu prensibe kesinlikle uyulmalıdır.cellenecektir.

Proje ekibi, bu yol haritasına uyarak sistematik ve planlı bir şekilde ilerlemelidir. Her aşamanın tamamlanması, bir sonraki aşamaya geçmek için önkoşuldur ve bu prensibe kesinlikle uyulmalıdır.