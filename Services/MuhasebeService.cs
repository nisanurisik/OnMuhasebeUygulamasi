using System;
using System.Collections.Generic;
using OnMuhasebeUygulama.Models;

namespace OnMuhasebeUygulama.Services;

class MuhasebeService
{
    public List<Cari> Cariler = new();
    public List<Stok> Stoklar = new();
    public List<Fatura> Faturalar = new();
    public List<Odeme> Odemeler = new();



    // ---CARİ İŞLEMLERİ---

    public void CariEkle()
    {
        Cari c = new();
        Console.Write("Cari ID'sini giriniz: ");
        c.Id = int.Parse(Console.ReadLine());
        Console.Write("Cari Adı'nı giriniz: ");
        c.Ad = Console.ReadLine();
        Cariler.Add(c);
    }

    public void CariSil()
    {
        Console.Write("Silinecek Cari ID'sini giriniz: ");
        int id = int.Parse(Console.ReadLine());

        Cari cari = Cariler.Find(c => c.Id == id);

        if (cari == null)
        {
            Console.WriteLine("Cari bulunamadı.");
            return;
        }

        if (cari.Borc != 0 || cari.Alacak != 0)
        {
            Console.WriteLine("Borcu veya alacağı olan cari silinemez.");
            return;
        }

        Cariler.Remove(cari);
        Console.WriteLine("Cari başarıyla silindi.");
    }

    public void CariListesi()
    {
        Console.WriteLine("\n--- CARİ LİSTESİ ---");

        if (Cariler.Count == 0)
        {
            Console.WriteLine("Kayıtlı cari yok.");
            return;
        }

        foreach (var c in Cariler)
        {
            Console.WriteLine($"ID: {c.Id} | Ad: {c.Ad}");
        }
    }

    public void CariRapor()
    {
        Console.Write("Raporlamak istediğiniz Cari ID'sini giriniz: ");
        int id = int.Parse(Console.ReadLine());

        Cari cari = Cariler.Find(c => c.Id == id);

        if (cari == null)
        {
            Console.WriteLine("Cari bulunamadı.");
            return;
        }

        Console.WriteLine($"\n{cari.Ad} | Borç: {cari.Borc} | Alacak: {cari.Alacak} | Bakiye: {cari.Bakiye}");
    }



    // ---STOK İŞLEMLERİ---

    public void StokEkle()
    {
        Stok s = new();
        Console.Write("Stok ID'sini giriniz: ");
        s.Id = int.Parse(Console.ReadLine());
        Console.Write("Stok Adı'nı giriniz: ");
        s.Ad = Console.ReadLine();
        Console.Write("Fiyatını giriniz: ");
        s.Fiyat = decimal.Parse(Console.ReadLine());
        Console.Write("Miktarını giriniz: ");
        s.Miktar = int.Parse(Console.ReadLine());
        Stoklar.Add(s);
    }

    public void StokSil()
    {
        Console.Write("Silinecek Stok ID'sini giriniz: ");
        int id = int.Parse(Console.ReadLine());

        Stok stok = Stoklar.Find(s => s.Id == id);

        if (stok == null)
        {
            Console.WriteLine("Stok bulunamadı.");
            return;
        }

        if (stok.Miktar > 0)
        {
            Console.WriteLine("Miktarı olan stok silinemez.");
            return;
        }

        Stoklar.Remove(stok);
        Console.WriteLine("Stok başarıyla silindi.");
    }

    public void StokListesi()
    {
        Console.WriteLine("\n--- STOK LİSTESİ ---");

        if (Stoklar.Count == 0)
        {
            Console.WriteLine("Kayıtlı stok yok.");
            return;
        }

        foreach (var s in Stoklar)
        {
            Console.WriteLine($"ID: {s.Id} | Ad: {s.Ad} | {s.Miktar}");
        }
    }

    public void StokRapor()
    {
        foreach (var s in Stoklar)
        {
            Console.WriteLine($"{s.Ad} | Miktar: {s.Miktar} | Fiyat: {s.Fiyat}");
        }
    }



    // ---FATURA İŞLEMLERİ---

    public void FaturaGir()
    {
        Console.WriteLine("Fatura Türünü Seçiniz:");
        Console.WriteLine("1- Alış Faturası");
        Console.WriteLine("2- Satış Faturası");
        Console.Write("Seçiminiz: ");

        int secim = int.Parse(Console.ReadLine());

        if (secim == 1)
            AlisFaturasiGir();
        else if (secim == 2)
            SatisFaturasiGir();
        else
            Console.WriteLine("Geçersiz seçim.");
    }

    public void AlisFaturasiGir()
    {
        Console.Write("Cari ID: ");
        int cariId = int.Parse(Console.ReadLine());
        Cari cari = Cariler.Find(c => c.Id == cariId);
        if (cari == null) return;

        decimal toplam = 0;

        while (true)
        {
            Console.Write("Alış faturası yapmak istediğiniz Stok ID'sini giriniz (İşlemi sonlandırmak için 0'a tıklayınız.): ");
            int stokId = int.Parse(Console.ReadLine());
            if (stokId == 0) break;

            Stok anaStok = Stoklar.Find(s => s.Id == stokId);
            if (anaStok == null) continue;

            Console.Write("Almak istediğiniz stok miktarını giriniz: ");
            int miktar = int.Parse(Console.ReadLine());

            if (miktar > anaStok.Miktar)
            {
                Console.WriteLine("Ana stokta yeterli miktar yok.");
                continue;
            }

            anaStok.Miktar -= miktar;
            Stok cariStok = CariStokBul(cari, anaStok);
            cariStok.Miktar += miktar;

            toplam += anaStok.Fiyat * miktar;
        }

        cari.Borc += toplam;
        Console.WriteLine("Alış faturası toplamı: " + toplam);

        Fatura fatura = new()
        {
            Id = Faturalar.Count + 1,
            CariId = cari.Id,
            CariAdi = cari.Ad,
            Tur = FaturaTuru.Alis,
            Toplam = toplam
        };

        Faturalar.Add(fatura);
    }

    public void SatisFaturasiGir()
    {
        Console.Write("Cari ID: ");
        int cariId = int.Parse(Console.ReadLine());
        Cari cari = Cariler.Find(c => c.Id == cariId);
        if (cari == null) return;

        decimal toplam = 0;

        while (true)
        {
            Console.Write("Satış yapmak istediğiniz stok ID'sini giriniz (İşlemi sonlandırmak için 0'a tıklayınız.): ");
            int stokId = int.Parse(Console.ReadLine());
            if (stokId == 0) break;

            Stok anaStok = Stoklar.Find(s => s.Id == stokId);
            if (anaStok == null) continue;

            Stok cariStok = cari.Stoklar.Find(s => s.Id == stokId);
            if (cariStok == null || cariStok.Miktar == 0)
            {
                Console.WriteLine("Caride yeterli stok yok.");
                continue;
            }

            Console.Write("Satış yapmak istediğiniz stok miktarını giriniz: ");
            int miktar = int.Parse(Console.ReadLine());

            if (miktar > cariStok.Miktar)
            {
                Console.WriteLine("Cari stok yetersiz.");
                continue;
            }

            cariStok.Miktar -= miktar;

            toplam += anaStok.Fiyat * miktar;
        }

        cari.Alacak += toplam;
        Console.WriteLine("Satış faturası toplamı: " + toplam);

        Fatura fatura = new()
        {
            Id = Faturalar.Count + 1,
            CariId = cari.Id,
            CariAdi = cari.Ad,
            Tur = FaturaTuru.Satis,
            Toplam = toplam
        };

        Faturalar.Add(fatura);
    }

    public void AlisFaturalariListesi()
    {
        foreach (var f in Faturalar.Where(f => f.Tur == FaturaTuru.Alis))
        {
            Console.WriteLine(
                $"ID:{f.Id} | Cari:{f.CariAdi} | Toplam:{f.Toplam}"
            );
        }
    }

    public void SatisFaturalariListesi()
    {
        foreach (var f in Faturalar.Where(f => f.Tur == FaturaTuru.Satis))
        {
            Console.WriteLine(
                $"ID:{f.Id} | Cari:{f.CariAdi} | Toplam:{f.Toplam}"
            );
        }
    }

    Stok CariStokBul(Cari cari, Stok anaStok)
    {
        Stok stok = cari.Stoklar.Find(s => s.Id == anaStok.Id);

        if (stok == null)
        {
            stok = new()
            {
                Id = anaStok.Id,
                Ad = anaStok.Ad,
                Fiyat = anaStok.Fiyat,
                Miktar = 0
            };
            cari.Stoklar.Add(stok);
        }

        return stok;
    }



    // ---ÖDEME İŞLEMLERİ---

    public void OdemeIslemleri()
    {
        Console.WriteLine("\n--- ÖDEME İŞLEMLERİ ---");
        Console.WriteLine("1- Ödeme Al");
        Console.WriteLine("2- Ödeme Yap");
        Console.Write("Seçiminiz: ");

        int secim = int.Parse(Console.ReadLine());

        if (secim == 1)
            AlinanOdemeGir();
        else if (secim == 2)
            YapilanOdemeGir();
        else
            Console.WriteLine("Geçersiz seçim.");
    }

    public void AlinanOdemeGir()
    {
        Console.Write("Cari ID: ");
        int id = int.Parse(Console.ReadLine());

        Cari cari = Cariler.Find(c => c.Id == id);
        if (cari == null)
        {
            Console.WriteLine("Cari bulunamadı.");
            return;
        }

        Console.Write("Tutarı giriniz: ");
        decimal tutar = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Tahsilat türünü seçiniz:");
        Console.WriteLine("1 - Nakit");
        Console.WriteLine("2 - Kart");
        int secim = int.Parse(Console.ReadLine());

        TahsilatTuru tahsilat = secim == 1 ? TahsilatTuru.Nakit : TahsilatTuru.Kart;

        cari.Alacak += tutar;

        if (tahsilat == TahsilatTuru.Nakit)
        cari.Kasa += tutar;

        else
            cari.Banka += tutar;

        Odemeler.Add(new()
        {
            Id = Odemeler.Count + 1,
            CariId = cari.Id,
            CariAdi = cari.Ad,
            Tur = OdemeTuru.Alinan,
            Tahsilat = tahsilat,
            Tutar = tutar
        });

        Console.WriteLine("Ödeme alındı.");
        Console.WriteLine($"Güncel Durum → Kasa: {cari.Kasa} | Banka: {cari.Banka} | Borç: {cari.Borc} | Alacak: {cari.Alacak} | Bakiye: {cari.Bakiye}");
    }

    public void YapilanOdemeGir()
    {
        Console.Write("Cari ID: ");
        int id = int.Parse(Console.ReadLine());

        Cari cari = Cariler.Find(c => c.Id == id);
        if (cari == null)
        {
            Console.WriteLine("Cari bulunamadı.");
            return;
        }

        Console.Write("Tutarı giriniz: ");
        decimal tutar = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Tahsilat türünü seçiniz:");
        Console.WriteLine("1 - Nakit");
        Console.WriteLine("2 - Kart");
        int secim = int.Parse(Console.ReadLine());

        TahsilatTuru tahsilat = secim == 1 ? TahsilatTuru.Nakit : TahsilatTuru.Kart;

        if (tutar > cari.Borc)
        {
            Console.WriteLine("Borçtan fazla ödeme yapılamaz.");
            return;
        }

        cari.Borc -= tutar;

        if (tahsilat == TahsilatTuru.Nakit)
        {
            cari.Kasa -= tutar;
        }
        
        else
        {
            cari.Banka -= tutar;
        }

        Odemeler.Add(new()
        {
            Id = Odemeler.Count + 1,
            CariId = cari.Id,
            CariAdi = cari.Ad,
            Tur = OdemeTuru.Yapilan,
            Tahsilat = tahsilat,
            Tutar = tutar
        });

        Console.WriteLine("Ödeme yapıldı.");
        Console.WriteLine($"Güncel Durum → Kasa: {cari.Kasa} | Banka: {cari.Banka} | Borç: {cari.Borc} | Alacak: {cari.Alacak} | Bakiye: {cari.Bakiye}");
    }

}
