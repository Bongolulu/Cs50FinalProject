namespace finance.Models;

public class PortfolioEintrag
{
    public string Symbol { get; set; }
    public int Anzahl { get; set; }
    public decimal Preis { get; set; }
    public string Name { get; set; }
}