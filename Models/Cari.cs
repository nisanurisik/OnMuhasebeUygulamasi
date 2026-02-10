namespace OnMuhasebeUygulama.Models;

class Cari
{
  public int Id;
  public string Ad;
  public decimal Borc;
  public decimal Alacak;
  public List<Stok> Stoklar = new();
  public decimal Kasa;
  public decimal Banka;
  public decimal Bakiye
  {
    get { return Alacak - Borc; }
  }
}
