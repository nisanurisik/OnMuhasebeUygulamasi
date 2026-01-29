namespace OnMuhasebeUygulama.Models;

class Cari
{
  public int Id;
  public string Ad;
  public decimal Borc;
  public decimal Alacak;

  public decimal Bakiye
  {
    get { return Alacak - Borc; }
  }
}
