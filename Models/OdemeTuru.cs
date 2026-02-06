namespace OnMuhasebeUygulama.Models;

enum OdemeTuru
{
    Alinan,
    Yapilan
}

class Odeme
{
    public int Id;
    public int CariId;
    public string CariAdi;
    public OdemeTuru Tur;
    public decimal Tutar;
}
