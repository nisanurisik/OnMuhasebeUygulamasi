namespace OnMuhasebeUygulama.Models;

enum FaturaTuru
{
  Alis,
  Satis
}

class Fatura
{
  public int Id;
  public int CariId;
  public string CariAdi;
  public FaturaTuru Tur;
  public decimal Toplam;
}
