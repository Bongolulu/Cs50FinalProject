using System.ComponentModel.DataAnnotations;
using finance.Helper;

namespace finance.Models;

public class TradeRequest
{
    [Required(ErrorMessage = "Symbol nicht eingegeben.")]
    public string Symbol { get; set; }

    [Required(ErrorMessage = "Anzahl nicht eingegeben")]
    [Range(1, int.MaxValue, ErrorMessage = "Bitte eine positive Anzahl eingeben.")]
    public int Anzahl { get; set; }
}