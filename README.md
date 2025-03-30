# Tedarix

Tedarix, bir tekstil firmasının üretim süreçlerini takip etmek amacıyla geliştirilmiş bir uygulamadır. Kesimden sevkiyata kadar tüm aşamalar, bu sistem üzerinden izlenebilir, üretim durumu kaydedilebilir ve raporlanabilir. Uygulama, yöneticilerin ve çalışanların üretim süreçlerini etkin bir şekilde yönetmelerini sağlar.

## Özellikler

- **Üretim Süreç Takibi:** Üretim aşamaları (kesim, dikim, paketleme vb.) sistem üzerinden takip edilebilir.
- **Sevkiyat Takibi:** Ürünlerin sevkiyat süreçleri sistemde izlenebilir ve raporlanabilir.
- **Kimlik Doğrulama ve Yetkilendirme:** Kullanıcılar, cookie tabanlı kimlik doğrulama ile giriş yapar. Yetkilendirme ise `Authorize` attribute ile sağlanır. Kullanıcı erişimleri rollerine göre düzenlenmiştir.
- **Veri Yönetimi:** Entity Framework Core kullanılarak veri yönetimi sağlanmıştır.
- **Raporlama:** Üretim ve sevkiyat süreçleri Excel formatında dışa aktarılabilir.

## Teknolojiler

- **Backend:** 
  - ASP.NET Core MVC
  - Entity Framework Core
  - MSSQL Server
  - Cookie Tabanlı Kimlik Doğrulama
  - `Authorize` Attribute ile Yetkilendirme

- **Frontend:** 
  - HTML, CSS
  - JavaScript (Etkileşimli işlemler için)


### Oluşturma zamanı 2023
