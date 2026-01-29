using System;
using System.Collections.Generic;
using OnMuhasebeUygulama.Models;

namespace OnMuhasebeUygulama.Services;

class MuhasebeService
{
  public List<Cari> Cariler = new();
  public List<Stok> Stoklar = new();

  public void CariEkle()
  {
    Cari c = new Cari();
    Console.Write("Cari ID: ");
    c.Id = int.Parse(Console.ReadLine());
    Console.Write("Cari Adı: ");
    c.Ad = Console.ReadLine();
    Cariler.Add(c);
  }

  public void StokEkle()
  {
    Stok s = new Stok();
    Console.Write("Stok ID: ");
    s.Id = int.Parse(Console.ReadLine());
    Console.Write("Stok Adı: ");
    s.Ad = Console.ReadLine();
    Console.Write("Fiyat: ");
    s.Fiyat = decimal.Parse(Console.ReadLine());
    Console.Write("Miktar: ");
    s.Miktar = int.Parse(Console.ReadLine());
    Stoklar.Add(s);
  }

  public void FaturaGir()
  {
    Console.Write("Cari ID: ");
    int cariId = int.Parse(Console.ReadLine());
    Cari cari = Cariler.Find(c => c.Id == cariId);

    if (cari == null)
    {
      Console.WriteLine("Cari bulunamadı.");
      return;
    }

    decimal toplam = 0;

    while (true)
    {
      Console.Write("Stok ID (bitirmek için 0): ");
      int stokId = int.Parse(Console.ReadLine());
      if (stokId == 0) break;

      Stok stok = Stoklar.Find(s => s.Id == stokId);
      if (stok == null)
      {
        Console.WriteLine("Stok bulunamadı.");
        continue;
      }

      Console.Write("Miktar: ");
      int miktar = int.Parse(Console.ReadLine());

      stok.Miktar -= miktar;
      toplam += stok.Fiyat * miktar;
    }

    cari.Borc += toplam;
    Console.WriteLine("Fatura Toplamı: " + toplam);
  }

  public void OdemeAl()
  {
    Console.Write("Cari ID: ");
    int id = int.Parse(Console.ReadLine());
    Cari cari = Cariler.Find(c => c.Id == id);

    if (cari == null)
    {
      Console.WriteLine("Cari bulunamadı.");
      return;
    }

    Console.Write("Ödeme Tutarı: ");
    decimal tutar = decimal.Parse(Console.ReadLine());
    cari.Alacak += tutar;
  }

  public void CariRapor()
  {
    foreach (var c in Cariler)
    {
      Console.WriteLine($"{c.Ad} | Borç: {c.Borc} | Alacak: {c.Alacak} | Bakiye: {c.Bakiye}");
    }
  }

  public void StokRapor()
  {
    foreach (var s in Stoklar)
    {
      Console.WriteLine($"{s.Ad} | Miktar: {s.Miktar} | Fiyat: {s.Fiyat}");
    }
  }
}
