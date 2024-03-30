using System.ComponentModel.DataAnnotations;

namespace finance.Models;

public class QuoteRequest
{
    [Required(ErrorMessage = "Symbol nicht eingegeben.")]
    public string Symbol { get; set; }
}