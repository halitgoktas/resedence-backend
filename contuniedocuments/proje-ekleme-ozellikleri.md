# Proje Ekleme Özellikleri - Rezidans ve Site Yönetim Sistemi

Bu doküman, Rezidans ve Site Yönetim Sistemi'ne eklenecek özellik ve iyileştirmeleri listelemektedir.

## 1. Multi-tenant İyileştirmeleri

- **JWT Token Entegrasyonu**: JWT tokenlardan tenant bilgilerinin (FirmaId ve SubeId) alınması ve middleware'de kullanılması
- **Multi-tenant Middleware Optimizasyonu**: Performans iyileştirmeleri ve hata yönetiminin geliştirilmesi
- **Firma ve Şube Bağımsız Tablolar**: Bazı referans tablolarının multi-tenant yapıdan muaf tutulması için filtre mekanizması

## 2. Servis Yönetim Modülü Geliştirmeleri

- **Servis Raporları İyileştirmesi**: ServiceReport veri modelinin genişletilmesi ve raporlama özelliklerinin artırılması
- **Teknik Servis İş Emri Sistemi**: İş emri oluşturma, takip etme ve sonuçlandırma süreçlerinin geliştirilmesi
- **Servis Talepleri Önceliklendirme**: Aciliyet durumuna göre otomatik önceliklendirme sistemi
- **Periyodik Bakım Planlaması**: Ekipmanlar için otomatik bakım hatırlatıcı ve planlama sistemi

## 3. Finansal Yönetim Özellikleri

- **Aidat Tahsilatı İzleme Paneli**: Aidat ödemelerinin durumunu gösteren dashboard
- **Gider Yönetimi Geliştirmeleri**: Kategori bazlı gider takibi ve analizi
- **Bütçe Planlama ve Takibi**: Aylık ve yıllık bütçe planlaması özellikleri
- **Geç Ödeme Bildirimleri**: Otomatik hatırlatıcı ve gecikme faizi hesaplama sistemi

## 4. Entegrasyon Özellikleri

- **E-Devlet Entegrasyonu**: E-Devlet platformu üzerinden kimlik doğrulama
- **Banka API Entegrasyonu**: Otomatik ödeme alma ve havale/EFT takibi
- **E-Fatura Entegrasyonu**: Otomatik e-fatura oluşturma ve gönderme
- **SMS ve E-posta Bildirim Sistemi**: Önemli durumlar için otomatik bildirim gönderimi

## 5. Raporlama ve Analitik Özellikleri

- **Gelişmiş Raporlama Motoru**: Özelleştirilebilir rapor şablonları ve filtreler
- **Finansal Analiz Raporları**: Gelir-gider analizi, nakit akışı ve tahsilat raporları
- **Doluluk ve Kira Analizi**: Daire bazlı doluluk oranları ve kira getirisi analizi
- **Servis Talepleri Analizi**: En çok talep edilen hizmetler ve çözüm süreleri analizi

## 6. Kullanıcı Deneyimi İyileştirmeleri

- **Mobil Uygulama**: Sakinler için mobil uygulama geliştirmesi (React Native)
- **Bildirim Merkezi**: Kullanıcılara özel bildirim yönetim paneli
- **Çoklu Tema Desteği**: Koyu/açık mod ve tema özelleştirme seçenekleri
- **Dashboard Özelleştirme**: Kullanıcıların kendi tercihlerine göre ayarlayabilecekleri dashboard

## 7. Güvenlik İyileştirmeleri

- **İki Faktörlü Kimlik Doğrulama**: SMS veya e-posta ile doğrulama
- **Gelişmiş Yetkilendirme Sistemi**: Rol bazlı erişim kontrolü ve özel izinler
- **Oturum Yönetimi İyileştirmeleri**: Aktif olmayan oturumları sonlandırma
- **Güvenlik Denetim Günlüğü**: Tüm kritik işlemlerin kayıt altına alınması

## 8. Performans Optimizasyonları

- **Database Sorgularının İyileştirilmesi**: İndeksleme ve sorgu optimizasyonu
- **Önbellek Mekanizması**: Sık kullanılan veriler için önbellek sistemi
- **API Yanıt Sürelerinin İyileştirilmesi**: Endpoint optimizasyonu
- **Asenkron İşlem Mekanizması**: Uzun süren işlemlerin arka planda çalıştırılması

## 9. Sistem Yönetim Özellikleri

- **Loglama Sistemi Geliştirmesi**: Detaylı log kayıtları ve log analizi
- **Otomatik Yedekleme Sistemi**: Düzenli veritabanı yedekleme ve geri yükleme
- **Sistem Durumu İzleme**: Performans metrikleri ve durum izleme dashboard'u
- **Batch İşlem Yönetimi**: Toplu işlemler için özel arayüz ve zamanlama

## 10. Envanter ve Demirbaş Yönetimi

- **Barkod Sistemi Entegrasyonu**: Demirbaşlar için barkod takip sistemi
- **Demirbaş Ömür Takibi**: Amortisman ve kullanım ömrü hesaplamaları
- **Envanter Sayım Modülü**: Dijital envanter sayım ve kontrol sistemi
- **Tedarikçi Yönetimi**: Tedarikçi bilgileri ve sipariş takibi

## 11. Daire ve Blok Yönetimi Geliştirmeleri

- **Daire Bilgi Kartı İyileştirmeleri**: Detaylı daire ve sakin bilgilerinin tek panelde görüntülenmesi
- **Blok Bazlı Gider Dağıtımı**: Farklı bloklara farklı gider dağıtım modelleri uygulama
- **Özelleştirilebilir Alan Tanımları**: Daireler için özel alan tanımlama imkanı
- **Kat Planı Görünümü**: Görsel kat planı üzerinden daire yönetimi

## 12. Dokümentasyon ve Yardım Sistemi

- **Kapsamlı Kullanım Kılavuzu**: Tüm modüller için detaylı kullanım belgeleri
- **Video Eğitimler**: Temel işlevler için eğitim videoları
- **İçerik Yardım Sistemi**: Uygulama içi yardım metinleri ve ipuçları
- **Sık Sorulan Sorular Bölümü**: Yaygın sorunlar ve çözümleri 