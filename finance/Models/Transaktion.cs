using NodaTime;

namespace finance.Models;

public class Transaktion
{    
    // Eigenschaften (sozusagen klassen-variablen)
    // bei allen Eigenschaften: Zugriff Typ Name z.b. public int wurst
   
    public Guid TransaktionId { get; set; }
    public Benutzer Benutzer { get; set; }
    public string Symbol { get; set; }
    public int Anzahl { get; set; }
    public decimal Preis { get; set; }

    public decimal Gesamtpreis
    {
        get { return Preis * Anzahl; }
    }
    public Instant DatumZeit { get; set; }

    
    // konstruktur (wird aufgerufen wenn man eine neue transaktion macht)
    public Transaktion()
    {
    }

    public Transaktion(Benutzer benutzer, string symbol, int anzahl, decimal preis)
    {
        if (anzahl == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(anzahl), "Anzahl 0 eingegeben.");
        }

        Benutzer = benutzer;
        Symbol = symbol;
        Anzahl = anzahl;
        Preis = preis;
        DatumZeit = SystemClock.Instance.GetCurrentInstant();   //Das gibt der User nicht ein. DateTime ist ein Typ und Klasse und DateTime.Now ist ein tatsächlicher Wert 
    }                                   // für eine Uhrzeit.

    
    // verhalten (methoden)

    public override string ToString()
    {
        return $"{DatumZeit} -> {Anzahl} von {Symbol} zum Preis von {Preis} ";
    }
    

}