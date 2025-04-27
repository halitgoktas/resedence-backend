# Residence Management Projesi Akıl Defteri

Bu belge, proje geliştirme sürecinde izlememiz gereken adımları, kritik noktaları ve dikkat edilmesi gereken hususları içermektedir.

## Veritabanı Bağlantı Bilgileri

- Sunucu: 120.0.0.1
- Kullanıcı: sa
- Şifre: q.1

## Çoklu Dil Desteği

Proje aşağıdaki dilleri destekleyecek şekilde geliştirilecektir:
- Türkçe (Varsayılan)
- İngilizce
- Almanca
- Rusça
- Arapça
- Farsça

## Veritabanı Yapısı

Veritabanı temel olarak iki tür tablodan oluşacaktır:
1. **Tanım Tabloları (Lookup Tables)**: 
   - Sabit değerleri içeren referans tabloları
   - Diğer tablolarda foreign key olarak kullanılacak
   - Örnek: Kullanıcı tipleri, ülkeler, şehirler, daireler

2. **Hareket Tabloları (Transaction Tables)**:
   - İşlem kayıtlarını tutan tablolar
   - Tanım tablolarından referans alan veriler
   - Örnek: Ödemeler, bakım kayıtları, şikayetler

### Tablo İlişkileri
- Her hareket tablosu ilgili tanım tablolarına FK ile bağlanacak
- Tüm tablolar normalize edilmiş olacak
- Silinme işlemleri için Cascade/Restrict kuralları belirlenecek

## Geliştirme Öncelikleri

- [x] Proje yapısı ve kurallarının belirlenmesi
- [x] Konfigürasyon dosyalarının hazırlanması
- [x] Çoklu dil yapısının kurulması
- [ ] Veritabanı şemasının oluşturulması
- [ ] Backend mimarisi (.NET 8)
- [ ] Frontend geliştirme (React)
- [ ] Mobil uygulama (React Native) - *patronun talimatına göre*

## Genel Hatırlatıcılar

- Tüm statik metinler çoklu dil desteğiyle hazırlanacak
- Her değişiklik Changelog'a eklenecek
- 400 satırdan fazla olan dosyalar parçalara ayrılacak
- Versiyon uyumsuzluğu olmayacak
- Proje ana yapısı korunacak
- Patronun talimatları önceliklidir
- Backend, Frontend ve Mobil kodları kendi klasörlerinde yazılacak
- Modern UI tasarımı kullanılacak
- Tanım ve hareket tabloları doğru ilişkilendirilecek

## .NET 8 Backend İçin Yapılacaklar

- [ ] Proje temel mimarisi oluşturulacak (N-Katmanlı Mimari)
- [ ] Veritabanı tasarımı yapılacak
  - [ ] Tanım tabloları oluşturulacak
  - [ ] Hareket tabloları oluşturulacak
  - [ ] Tablolar arası ilişkiler kurulacak
- [ ] Entity sınıfları hazırlanacak
- [ ] Migrations yapılandırması
- [ ] Repository ve Unit of Work implementasyonu
- [ ] API controller yapısı kurulacak
- [ ] Authentication sistemi geliştirilecek
- [ ] Middleware'ler eklenecek
- [ ] API dokümantasyonu hazırlanacak (Swagger)
- [ ] Hata yönetimi sistemi kurulacak
- [ ] Loglama mekanizması entegre edilecek
- [ ] Unit testler yazılacak

## React Frontend İçin Yapılacaklar

- [ ] Component mimarisi kurulacak
- [ ] Routing yapısı kurulacak
- [ ] State yönetimi (Redux veya Context API)
- [ ] API entegrasyonu yapılacak
  - [ ] Backend API'lerine bağlantı
  - [ ] Veri alışverişi
- [ ] Form yönetimi ve validasyon
- [ ] UI/UX tasarımı
  - [ ] Modern ve responsive tasarım
  - [ ] Animasyonlar ve geçişler
- [ ] Responsive tasarım
- [ ] Unit testler yazılacak

## React Native Mobil İçin Yapılacaklar

- [ ] Ekran tasarımları hazırlanacak
- [ ] Navigasyon yapısı kurulacak
- [ ] API entegrasyonu yapılacak
- [ ] State yönetimi
- [ ] Native özellikler entegre edilecek
- [ ] Platform özel düzenlemeler
- [ ] Unit testler yazılacak

## Klasör Yapısı ve Kod Yerleşimi

- Backend kodları sadece Backend/ klasöründe
- Frontend kodları sadece Frontend/ klasöründe
- Mobil kodları sadece Mobil/ klasöründe

Bu kurala kesinlikle uyulmalı, hiçbir durumda yanlış klasörde geliştirme yapılmamalıdır.

## Dikkat Edilecek Kritik Noktalar

1. **Veritabanı İlişkileri**
   - İlişkilerin doğru tanımlanması
   - Performans için indeksleme
   - Migration stratejisi
   - Tanım ve hareket tablolarının doğru ilişkilendirilmesi

2. **API Güvenliği**
   - Token bazlı auth
   - Rate limiting
   - CORS yapılandırması

3. **Frontend Performansı**
   - Bundle boyutu optimizasyonu
   - Memoization
   - Code splitting
   - Lazy loading

4. **Kod Kalitesi**
   - Clean code prensipleri
   - SOLID prensipleri
   - DRY prensibi

5. **Pazara Sunum**
   - CI/CD pipeline
   - Deployment stratejisi
   - Monitoring ve loglama

## Kullanıcı Hikayeleri (User Stories)

- [ ] Kullanıcı sisteme giriş yapabilmeli
- [ ] Kullanıcı profilini düzenleyebilmeli
- [ ] Yönetici kullanıcı ekleyebilmeli/düzenleyebilmeli/silebilmeli
- [ ] Kullanıcı şifresini değiştirebilmeli/sıfırlayabilmeli
- [ ] *Diğer kullanıcı hikayeleri proje dokümanı geldiğinde eklenecek*

## Bağımlılık Kontrol Listesi

- [ ] Backend paketleri birbiriyle uyumlu olmalı
- [ ] Frontend paketleri birbiriyle uyumlu olmalı
- [ ] Mobil paketleri birbiriyle uyumlu olmalı
- [ ] Dış API entegrasyonları listelenmeli
- [ ] Lisans kontrolü yapılmalı

## Hatırlatıcılar

- Her kod dosyasına detaylı yorum eklenecek
- Yapılan işlemler CHANGELOG.md'ye kaydedilecek
- Root dizinde geliştirme yapılmayacak
- Uygulanacak kodlama standartları CODING_GUIDELINES.md dosyasındaki gibi olacak
- Kurallar RULES.md dosyasında belirtildiği gibi takip edilecek
- PowerShell'de ';' kullanarak komutlar yazılacak 