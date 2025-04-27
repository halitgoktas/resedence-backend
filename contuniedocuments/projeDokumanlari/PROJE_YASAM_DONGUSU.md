# PROJE YAŞAM DÖNGÜSÜ

Bu doküman, Rezidans ve Site Yönetim Sistemi projesinin geliştirilme sürecindeki yaşam döngüsünü ve fazlarını açıklar.

## 1. Proje Fazları

Proje, aşağıdaki temel fazlardan oluşacaktır:

### 1.1. Faz 1: Temel Altyapı ve Backend Geliştirme

**Durum:** Başlangıç

**Süre:** 4 hafta

**Hedefler:**
- Proje yapısının oluşturulması
- Veritabanı modellerinin tasarlanması ve migration'ların hazırlanması
- Temel repository ve service katmanlarının oluşturulması
- Temel API endpointlerinin oluşturulması
- JWT tabanlı kimlik doğrulama sisteminin implementasyonu
- Multi-tenant yapının kurulması
- Temel test senaryolarının yazılması

### 1.2. Faz 2: Backend Tamamlama ve İleri Özellikler

**Durum:** Beklemede

**Süre:** 3 hafta

**Hedefler:**
- Tüm veritabanı modellerinin tamamlanması
- Eksik API endpointlerinin eklenmesi
- İki faktörlü kimlik doğrulama (2FA) desteği
- Çoklu dil ve para birimi desteği
- Otomatik kur çekme ve dönüştürme özelliği
- KBS ve diğer entegrasyonların tamamlanması
- Backend testlerin tamamlanması

### 1.3. Faz 3: Frontend Web Uygulaması Geliştirme

**Durum:** Beklemede

**Süre:** 5 hafta

**Hedefler:**
- Temel arayüz bileşenlerinin geliştirilmesi
- Kimlik doğrulama sayfaları ve yetkilendirme sistemi
- Daire ve sakin yönetimi ekranları
- Finansal işlemler ve aidat yönetimi ekranları
- Rezervasyon ve hizmet yönetimi ekranları
- Dashboard ve raporlama sayfaları
- Çoklu dil desteği
- Responsive tasarım

### 1.4. Faz 4: Optimizasyon, Test ve Dokümantasyon

**Durum:** Beklemede

**Süre:** 2 hafta

**Hedefler:**
- Performans optimizasyonları
- Güvenlik testleri
- End-to-end testlerin tamamlanması
- Kullanıcı dokümantasyonunun hazırlanması
- Geliştirici dokümantasyonunun tamamlanması
- API dokümantasyonunun güncellenmesi

### 1.5. Faz 5: Mobil Uygulama ve İleri Özellikler

**Durum:** Beklemede

**Süre:** 6 hafta

**Hedefler:**
- React Native ile mobil uygulama geliştirme
- Push notification entegrasyonu
- Offline çalışma modu
- Kamera ve konum servisleri entegrasyonu
- Biyometrik kimlik doğrulama
- Mobil-spesifik özellikler

## 2. Geliştirme Metodolojisi

### 2.1. Agile ve Scrum

Proje, Agile prensipleri ve Scrum çerçevesi kullanılarak geliştirilecektir:

- **Sprint Süresi:** 2 hafta
- **Günlük Stand-up Toplantıları:** Her sabah 15 dakika
- **Sprint Planlama:** Her sprint başlangıcında
- **Sprint Değerlendirme:** Her sprint sonunda
- **Sprint Retrospektifi:** Her sprint sonunda

### 2.2. İş Akışı

Her özellik veya görev için aşağıdaki iş akışı izlenecektir:

1. **Planlama:** İş gereksinimlerinin analizi ve görevlerin tanımlanması
2. **Tasarım:** Teknik tasarım ve çözüm yaklaşımının belirlenmesi
3. **Geliştirme:** Kodlama ve test
4. **İnceleme:** Kod incelemesi ve kalite kontrolü
5. **Test:** Test senaryolarının çalıştırılması
6. **Dağıtım:** Geliştirme ortamına dağıtım
7. **Kabul Testi:** Kullanıcı kabul testleri
8. **Canlıya Alma:** Üretim ortamına dağıtım

## 3. Test Stratejisi

### 3.1. Test Seviyeleri

Proje geliştirme sürecinde aşağıdaki test seviyeleri uygulanacaktır:

- **Birim Testleri:** Metot ve fonksiyon seviyesinde
- **Entegrasyon Testleri:** Bileşenler arası etkileşimde
- **Sistem Testleri:** Uçtan uca işlevsellik testleri
- **Kabul Testleri:** Kullanıcı gereksinimleri bazında

### 3.2. Test Teknikleri

- **TDD (Test-Driven Development):** Test önce yaklaşımı
- **Regression Testing:** Değişiklik sonrası etki analizi
- **Performance Testing:** Performans ve yük testleri
- **Security Testing:** Güvenlik açığı taramaları

## 4. Deployment Stratejisi

### 4.1. Ortamlar

- **Geliştirme Ortamı:** Geliştiricilerin çalıştığı ortam
- **Test Ortamı:** Test ekibinin test süreçlerini yürüttüğü ortam
- **Staging Ortamı:** Üretim ortamına benzer, demo amaçlı kullanılan ortam
- **Üretim Ortamı:** Son kullanıcıların eriştiği canlı ortam

### 4.2. CI/CD Pipeline

- **Continuous Integration:** Her commit'te build ve test otomasyonu
- **Continuous Deployment:** Test başarılı olursa, otomatik dağıtım
- **Deployment Automation:** Dağıtım işlemlerinin otomatize edilmesi

## 5. Kalite Güvence

### 5.1. Kod Kalitesi

- **Code Review:** Tüm kod değişiklikleri incelenecek
- **Static Code Analysis:** SonarQube veya benzer araçlar kullanılacak
- **Coding Standards:** Tutarlı kod stili ve formatlama
- **Technical Debt Management:** Teknik borçların yönetimi

### 5.2. Dokümantasyon Kalitesi

- **API Dokümantasyonu:** Swagger ile otomatik API dokümantasyonu
- **Kod İçi Yorumlar:** Tüm public metotlar ve kompleks kodlar için açıklayıcı yorumlar
- **Kullanıcı Kılavuzları:** Son kullanıcılar için detaylı kullanım kılavuzları
- **Sistem Dokümantasyonu:** Geliştirici ve yöneticiler için teknik dokümantasyon

## 6. Proje Takibi ve İletişim

### 6.1. Takip Araçları

- **İş Takibi:** Jira veya Azure DevOps
- **Kod Yönetimi:** Git (GitHub/GitLab/Azure DevOps)
- **Doküman Yönetimi:** Confluence veya benzeri wiki platformu

### 6.2. İletişim Kanalları

- **Günlük Toplantılar:** Stand-up toplantıları
- **Sprint Toplantıları:** Sprint planlama, değerlendirme ve retrospektif
- **Anlık İletişim:** Teams, Slack veya benzeri anlık mesajlaşma platformu
- **E-posta:** Resmi iletişim ve bildirimler için

## 7. Riskler ve Azaltma Stratejileri

### 7.1. Tanımlanmış Riskler

- **Teknik Riskler:** Teknoloji seçimleri, entegrasyon zorlukları
- **Kaynak Riskleri:** Ekip üyelerinin değişmesi, bilgi transferi zorlukları
- **Zaman Riskleri:** Geciken görevlerin projeyi etkilemesi
- **Kapsam Riskleri:** Kapsam genişlemesi (scope creep)

### 7.2. Risk Azaltma Stratejileri

- **Erken Prototipleme:** Teknik riskleri erken aşamada tespit etmek
- **Bilgi Paylaşımı:** Ekip içi eğitim ve dokümantasyon
- **Çeviklik:** Değişen gereksinimlere hızlı adaptasyon
- **Sıkı Kapsam Yönetimi:** Değişiklik isteklerinin etki analizinin yapılması

Bu yaşam döngüsü, projenin başarılı bir şekilde tamamlanması için rehber olacaktır. Proje ilerledikçe bu doküman güncellenecek ve geliştirilecektir. 