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
      Console.WriteLine("2- Stok Ekle");
      Console.WriteLine("3- Fatura Girişi");
      Console.WriteLine("4- Ödeme Al");
      Console.WriteLine("5- Cari Raporu");
      Console.WriteLine("6- Stok Raporu");
      Console.WriteLine("0- Çıkış");

      int secim = int.Parse(Console.ReadLine());

      if (secim == 1) service.CariEkle();
      else if (secim == 2) service.StokEkle();
      else if (secim == 3) service.FaturaGir();
      else if (secim == 4) service.OdemeAl();
      else if (secim == 5) service.CariRapor();
      else if (secim == 6) service.StokRapor();
      else if (secim == 0) break;
    }
  }
}
