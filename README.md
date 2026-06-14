# .NET MAUI Kapsamlı Uygulama

Bu proje, C# ve .NET MAUI platformu kullanılarak geliştirilmiş, çok sayfalı mimariye sahip ve içerisinde birçok farklı servis barındıran kapsamlı bir mobil/masaüstü (cross-platform) uygulamasıdır.

## Proje İçeriği

* **Kimlik Doğrulama:** Kullanıcı giriş ve kayıt işlemleri `FirebaseAuthentication.net` kullanılarak güvenli bir şekilde sağlanmıştır.
* **API Entegrasyonları:**
  * **Döviz Kurları:** Canlı döviz kurları REST API üzerinden çekilerek anlık güncellenmektedir.
  * **Hava Durumu:** Meteoroloji (MGM) API'si kullanılarak hava durumu verileri çekilmekte, kullanıcının eklediği şehirler yerel cihaza JSON formatında kaydedilerek veri kalıcılığı sağlanmaktadır.
* **RSS Veri İşleme (Haberler):** TRT Haber RSS kaynakları parse edilerek kategorize edilmiş, haber detayları ve `Share API` ile haberi farklı platformlarda paylaşma özelliği eklenmiştir.
* **Veritabanı Destekli To-Do List:** Kullanıcıların görev ekleyip silebildiği "Yapılacaklar" modülü doğrudan uzak veritabanı (Firebase) ile senkronize çalışmaktadır. Ayrıca veri güvenliği için silme işlemlerinde kullanıcı onay mekanizması eklenmiştir.
* **Dinamik Tema Yönetimi:** Uygulama içerisinde kullanıcı tercihine bağlı olarak anlık Açık/Koyu (Light/Dark) tema geçişi yapılabilmektedir.

## Kullanılan Teknolojiler
* **Geliştirme Platformu:** .NET MAUI
* **Programlama Dili:** C#
* **Arayüz İşaretleme Dili:** XAML
* **Veritabanı & Güvenlik:** Firebase Authentication, Firebase
* **Veri İşleme:** REST API, RSS Parsing, JSON Serialization
