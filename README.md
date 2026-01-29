# ÖN MUHASEBE KONSOL UYGULAMASI

Bu ödevde temel düzeyde ön muhasebe konsol uygulaması geliştirdim.

Uygulamamda classları Models klasöründe, yapılan işlemlerin olduğu fonksiyonları da Service klasöründe tuttum. Kodun daha kolay okunması için bu şekilde klasörlendirdim. 

### Cari Sınıfı: 
Cari sınıfında isim, borç, alacak ve bakiye bilgilerini tuttum.

### Stok Sınıfı: 
Stok sınıfında stok adı, fiyat ve miktar bilgilerini tuttum. 

Fonksiyonları yani uygulamanın iş mantığını MuhasebeService dosyasında yazdım. Bu dosyada cari ekleme, stok ekleme, ödeme alma, fatura girişi, cari rapor, stok rapor işlemlerini yaptırdım.
Program.cs dosyasında kullanıcıya bir seçim menüsü sunarak yapmak istediği işlemi seçtirdim ve gerekli bilgileri girmesini istedim.

Kısaca çalışma  mantığından bahsedecek olursam;
Cari kartta borç, alacak ve bakiye bilgileri tutuluyor. Fatura girdiğimizde borç artıyor, ödeme alındığında ise borç azalıyor ve alacak artıyor.
Bir kullanıcı fatura girmek istediğinde öncelikli olarak bir cari seçmesi gerekiyor. Daha sonrasında stok seçmesi gerekiyor. Stok seçilince stok raporunda o ürünün stok miktarı azalıyor. Stok miktarı * stok fiyatı da carinin borcuna ekleniyor ve bakiyeden düşüyor.
Ödeme işlemi yapılmak istedndiğinde  ilk olarak cari seçmesi gerekiyor, daha sonra ödeme tutarı giriliyor ve bu tutar cari raporunda alacak olarak görünüyor ve bakiyeye ekleniyor.
