using System;
using OnMuhasebeUygulama.Services;

class Program
{
    static void Main()
    {
        MuhasebeService service = new MuhasebeService();

        while (true)
        {
            Console.WriteLine("\n ÖN MUHASEBE ");
            Console.WriteLine("1- Cari Ekle");
            Console.WriteLine("2- Cari Sil");
            Console.WriteLine("3- Cari Listesi");
            Console.WriteLine("4- Cari Raporu");
            Console.WriteLine("5- Stok Ekle");
            Console.WriteLine("6- Stok Sil");
            Console.WriteLine("7- Stok Listesi");
            Console.WriteLine("8- Stok Raporu");
            Console.WriteLine("9- Fatura Girişi");
            Console.WriteLine("10- Alış Faturalarını Listele");
            Console.WriteLine("11- Satış Faturalarını Listele");
            Console.WriteLine("12- Ödeme İşlemleri");
            Console.WriteLine("0- Çıkış");

            int secim = int.Parse(Console.ReadLine());

            if (secim == 1) service.CariEkle();
            else if (secim == 2) service.CariSil();
            else if (secim == 3) service.CariListesi();
            else if (secim == 4) service.CariRapor();
            else if (secim == 5) service.StokEkle();
            else if (secim == 6) service.StokSil();
            else if (secim == 7) service.StokListesi();
            else if (secim == 8) service.StokRapor();
            else if (secim == 9) service.FaturaGir();
            else if (secim == 10) service.AlisFaturalariListesi();
            else if (secim == 11) service.SatisFaturalariListesi();
            else if (secim == 12) service.OdemeIslemleri();
            else if (secim == 0) break;
            else
            {
                Console.WriteLine("Geçersiz seçim, lütfen menüden seçim yapınız.");
            }
        }
    }
}
