# Rezidans ve Site Yönetim Sistemi - Detaylı Özellikler

## Geliştirilen Model Yapıları ve Amaçları

### SiteManager ve MultiSiteManager Modelleri

// SiteManager modeli, tek bir siteyi yöneten yöneticilerin bilgilerini saklar
// MultiSiteManager modeli, birden fazla siteyi aynı anda yönetebilen yöneticileri temsil eder
// Bu modeller, farklı yönetim seviyelerinin tanımlanması ve yetkilendirilmesi amacıyla geliştirilmiştir

**SiteManager Modelinin Amacı**:
- Tek bir siteye odaklanmış yöneticilerin tanımlanması
- Site bazında sorumlulukların ve yetkilerin belirlenmesi 
- Site özelinde operasyonel yönetim sağlanması
- Site sakinleriyle doğrudan iletişim kurulabilmesi

**MultiSiteManager Modelinin Amacı**:
- Aynı şirkete ait birden fazla sitenin ortak yönetiminin sağlanması
- Şirket genelinde standart uygulamaların ve politikaların uygulanması
- Kaynakların sitelere dağıtımının merkezi yönetimi
- Çoklu site operasyonlarında koordinasyon ve denetim sağlanması

**Geliştirilen Ortak Özellikler**:
- Kimlik ve iletişim bilgilerinin saklanması (Ad, Soyad, TC, Telefon, E-posta)
- Yetki seviyelerinin ve özel izinlerinin tanımlanması
- Çalışma koşullarının belirlenmesi (Çalışma saatleri, maaş bilgileri)
- Performans değerlendirme metrikleri
- KBS (Kimlik Bildirim Sistemi) entegrasyonu için gerekli alanlar

### ManagerTask, ManagerTaskHistory ve ManagerTaskComment Modelleri

// ManagerTask modeli, yöneticilere atanan görevlerin tanımlarını ve durumlarını saklar
// ManagerTaskHistory modeli, görevlerdeki değişikliklerin tarihçesini tutar
// ManagerTaskComment modeli, görevler hakkında yapılan yorumları ve iletişimi kaydeder

**ManagerTask Modelinin Amacı**:
- Yöneticilere görev atanması ve takibinin yapılması
- Site operasyonlarının planlı şekilde yürütülmesi
- Bakım, onarım ve diğer yönetim görevlerinin zamanında tamamlanmasının sağlanması
- Görevlerin önceliklendirilmesi ve kategorize edilmesi

**ManagerTaskHistory Modelinin Amacı**:
- Görev durumlarındaki değişikliklerin izlenmesi
- Görevlerdeki ilerlemenin kayıt altına alınması
- Sorumluluk takibi için denetim izi oluşturulması
- Görev geçmişinden öğrenilerek süreç iyileştirmelerinin yapılması

**ManagerTaskComment Modelinin Amacı**:
- Görevler hakkında iletişimin sağlanması
- Görev yürütücüleri ve atayanlar arasında bilgi paylaşımı
- Problemlerin ve çözümlerin dokümante edilmesi
- Görev sürecinde yaşanan durumların kayıt altına alınması

**Geliştirilen Özellikler**:
- Görev atama ve takip sistemi
- Görev önceliklendirme ve kategorizasyon
- Otomatik bildirimler ve hatırlatıcılar
- Görev ilerleme takibi ve tamamlanma yüzdesi
- Görev tarihçesi ve değişikliklerin izlenmesi
- Görevler üzerinde iletişim ve yorum yapabilme

### ManagerActivityLog Modeli

// ManagerActivityLog modeli, yöneticilerin sistem üzerindeki tüm aktivitelerini kaydeder

**Amacı**:
- Yöneticilerin gerçekleştirdiği işlemlerin kayıt altına alınması
- Güvenlik ve denetim için aktivite izleme
- İşlem hatalarının tespit edilmesi ve düzeltilmesi
- Performans değerlendirmesi için veri sağlanması

**Geliştirilen Özellikler**:
- Aktivite tipi ve detaylarının kaydedilmesi
- Tarih, saat ve kullanıcı bilgilerinin tutulması
- IP adresi ve cihaz bilgilerinin kaydedilmesi
- Etkilenen kayıtların ve değişikliklerin izlenmesi
- Aktivite önem seviyesi belirleme
- Aktivite inceleme ve değerlendirme süreci

## Ekranlar ve İşlevler

### 1. Yönetici Yönetim Ekranları

#### 1.1. Site Yöneticileri Listesi

**İşlevleri**:
- Tüm site yöneticilerinin listesi ve filtreleme
- Yönetici durumlarının (aktif/pasif) görüntülenmesi
- Yöneticilerin sorumlu oldukları sitelerin gösterimi
- Yönetici performans puanlarının görüntülenmesi
- Listelenen yöneticiler için toplu işlemler (aktifleştirme, pasifleştirme)

#### 1.2. Site Yöneticisi Detay/Düzenleme Ekranı

**İşlevleri**:
- Kişisel bilgilerin görüntülenmesi ve düzenlenmesi
- Yönetici fotoğrafı yükleme ve güncelleme
- İletişim bilgilerinin yönetimi
- Yöneticiye atanan sitenin değiştirilmesi
- Yetki seviyelerinin ve özel izinlerin tanımlanması
- Çalışma koşullarının (saat, ücret) belirlenmesi
- Yöneticiye ait görevlerin listesi ve filtrelenmesi
- Yönetici aktivite geçmişinin incelenmesi
- KBS bildirimi yapma ve durumunu görüntüleme

#### 1.3. Çoklu Site Yöneticileri Listesi

**İşlevleri**:
- Tüm çoklu site yöneticilerinin listesi ve filtreleme
- Yöneticilerin yönettikleri sitelerin sayısı ve listesi
- Performans puanları ve genel durum bilgileri
- Site dağılımı ve sorumluluk alanları görüntüleme

#### 1.4. Çoklu Site Yöneticisi Detay/Düzenleme Ekranı

**İşlevleri**:
- Kişisel ve iletişim bilgilerinin yönetimi
- Yönetiminden sorumlu olduğu sitelerin atanması ve çıkarılması
- Yönettiği sitelerin performans göstergelerinin görüntülenmesi
- Site bazında yetkilendirme yapılabilmesi
- Tüm siteler veya belirli siteler için görev ataması
- Aktivite geçmişinin site bazında filtrelenmesi

### 2. Görev Yönetim Ekranları

#### 2.1. Görev Listesi

**İşlevleri**:
- Tüm görevlerin listesi ve çoklu filtreleme seçenekleri
- Görev durumlarına göre renklendirme ve gruplandırma
- Öncelik seviyelerine göre sıralama ve filtreleme
- Gecikmiş görevlerin vurgulanması ve uyarılar
- Yaklaşan son tarihi olan görevlerin bildirilmesi
- Görev kategorilerine göre dağılım grafikleri
- Görevlerin atandığı yöneticilere göre filtreleme

#### 2.2. Görev Oluşturma Ekranı

**İşlevleri**:
- Görev başlığı ve detaylı açıklama girişi
- Kategorilerin ve alt kategorilerin seçimi
- Öncelik seviyesinin belirlenmesi
- İlgili site, bina veya dairenin seçilmesi
- Planlanan başlangıç ve bitiş tarihlerinin ayarlanması
- Görevin atanacağı yöneticinin seçilmesi
- Dosya ve belge ekleyebilme
- Tahmini maliyet ve bütçe bilgilerinin girişi
- Tekrarlanan görev olarak ayarlama seçeneği

#### 2.3. Görev Detay ve Düzenleme Ekranı

**İşlevleri**:
- Görev detaylarının görüntülenmesi ve düzenlenmesi
- Görev durumunun güncellenmesi
- İlerleme yüzdesinin ayarlanması
- Görev geçmişinin incelenmesi
- Yorumlar ve iletişim bölümü
- İlgili dosyaların görüntülenmesi ve yeni dosya ekleme
- Görevin yeniden atanması veya iptal edilmesi
- Tamamlanan görevler için sonuç raporu oluşturma
- Görev tamamlandığında otomatik bildirim gönderme

#### 2.4. Görev Tarihçesi Görüntüleme

**İşlevleri**:
- Görev üzerinde yapılan tüm değişikliklerin kronolojik listesi
- Değişikliği yapan kullanıcıların bilgileri
- Değişiklik öncesi ve sonrası değerlerin karşılaştırması
- Değişikliklerin nedenleri ve açıklamaları
- Zaman damgası ve IP bilgisi ile güvenlik takibi

#### 2.5. Görev Yorumları ve İletişim

**İşlevleri**:
- Görev hakkında yorum ve not ekleme
- Yöneticiler arasında görev hakkında iletişim
- Yorumlara dosya ekleyebilme
- Yorum bildirimleri ve abonelik sistemi
- Yorumları görüntüleme izinlerinin yönetimi (özel/genel)

### 3. Aktivite İzleme Ekranları

#### 3.1. Yönetici Aktivite Logları Listesi

**İşlevleri**:
- Tüm yönetici aktivitelerinin kronolojik listesi
- Yönetici, site, tarih, aktivite tipi gibi filtreler
- Aktivite önem seviyelerine göre renklendirme
- Anormal aktivitelerin vurgulanması
- Aktivitelerin detaylı incelenmesi
- Aktivite istatistikleri ve raporlama

#### 3.2. Aktivite Detay Görüntüleme

**İşlevleri**:
- Aktivite detaylarının tam görüntülenmesi
- Aktiviteyi gerçekleştiren yönetici bilgileri
- Etkilenen kayıt veya işlem detayları
- Değişiklik öncesi ve sonrası değerlerin karşılaştırması
- IP adresi ve cihaz bilgilerinin incelenmesi
- Aktiviteden etkilenen ilgili kayıtlara hızlı erişim

### 4. Raporlama Ekranları

#### 4.1. Yönetici Performans Raporu

**İşlevleri**:
- Yönetici bazında görev tamamlama performansı
- Zamanında tamamlanan görev oranları
- Ortalama görev tamamlama süreleri
- Görev kategorilerine göre performans dağılımı
- Sakin memnuniyet puanları ile ilişkilendirme
- Zaman içindeki performans değişim grafikleri

#### 4.2. Görev Analiz Raporu

**İşlevleri**:
- Görev türlerine göre dağılım ve tamamlanma oranları
- Gecikmiş görevlerin analizi ve nedenleri
- Görev tamamlama sürelerinin analizi
- Maliyetlerin planlanan ve gerçekleşen karşılaştırması
- Görev yoğunluğunun zaman içindeki dağılımı
- Problem yaşanan alanların tespiti

#### 4.3. Site Yönetim Etkinliği Raporu

**İşlevleri**:
- Site bazında yönetim performansı karşılaştırması
- Problem çözüm süreleri ve oranları
- Sakin talep ve şikayetlerinin çözülme oranları
- Planlı bakım görevlerinin zamanında yapılma oranları
- Site bazında maliyetlerin karşılaştırılması
- Yönetici değişimlerinin performansa etkisi

## Entegrasyonlar ve Teknik Özellikler

### 1. KBS (Kimlik Bildirim Sistemi) Entegrasyonu

**İşlevleri**:
- Yönetici kimlik bilgilerinin otomatik KBS'ye bildirilmesi
- KBS bildirim durumunun izlenmesi ve raporlanması
- Bildirim geçmişinin tutulması ve arşivlenmesi
- Yasal gerekliliklere uyumun sağlanması

### 2. Bildirim Sistemi

**İşlevleri**:
- Görev atama ve değişikliklerde otomatik bildirimler
- Hatırlatıcılar ve son tarih yaklaşan görevler için uyarılar
- Önemli aktiviteler için yöneticilere anlık bildirimler
- E-posta, SMS ve uygulama içi bildirim seçenekleri
- Bildirim tercihlerinin kişiselleştirilmesi

### 3. Doküman Yönetimi

**İşlevleri**:
- Görevlere dosya ve belge ekleme
- Yönetici sözleşmeleri ve belgelerin saklanması
- OCR ile doküman içeriğinde arama yapabilme
- Belge sürüm kontrolü ve geçmiş görüntüleme
- Belgelerin kategorize edilmesi ve etiketlenmesi

### 4. Yetkilendirme ve Güvenlik

**İşlevleri**:
- Rol bazlı yetkilendirme sistemi
- İnce granüllü erişim kontrolü
- IP bazlı erişim kısıtlamaları
- İki faktörlü kimlik doğrulama
- Aktivite ve güvenlik logları

## Model Geliştirmelerinin Genel Amaçları

1. **Etkin Yönetim Yapısı**: Farklı yönetim seviyelerini (tek site, çoklu site) destekleyen esnek bir yapı kurulması

2. **Görev Takibi ve Sorumluluk**: Görevlerin atanması, izlenmesi ve tamamlanmasının sağlanarak operasyonel verimliliğin artırılması

3. **Şeffaflık ve Denetim**: Tüm yönetici aktivitelerinin kaydedilmesi ile şeffaflık ve hesap verilebilirliğin sağlanması

4. **Veri Güvenliği**: Hassas bilgilerin güvenli şekilde saklanması ve erişimin kontrol edilmesi

5. **Yasal Uyumluluk**: KBS gibi yasal gerekliliklerin sistem üzerinden karşılanması

6. **İletişim ve Koordinasyon**: Yöneticiler arasında ve diğer paydaşlarla etkin iletişimin sağlanması

7. **Performans İzleme**: Yönetici ve site performanslarının objektif metriklere dayalı olarak değerlendirilmesi

8. **Süreç İyileştirme**: Toplanan veriler üzerinden analiz yapılarak yönetim süreçlerinin sürekli iyileştirilmesi

Bu detaylı sistem, site ve rezidans yönetiminin profesyonelleşmesini, sakin memnuniyetinin artmasını ve maliyetlerin optimize edilmesini sağlayacaktır. Modüler yapısı sayesinde, farklı büyüklükteki site yönetim şirketlerinin ihtiyaçlarına uyarlanabilir ve zaman içinde genişletilebilir özelliklere sahiptir. 