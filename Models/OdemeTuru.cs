namespace OnMuhasebeUygulama.Models;

enum OdemeTuru
{
    Alinan,
    Yapilan
}

enum TahsilatTuru
{
    Nakit,
    Kart
}

class Odeme
{
    public int Id;
    public int CariId;
    public string CariAdi;
    public OdemeTuru Tur;
    public TahsilatTuru Tahsilat;
    public decimal Tutar;
}
