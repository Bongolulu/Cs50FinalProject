

using System.ComponentModel.DataAnnotations;
using finance.Helper;

namespace Classes;

public class RegisterRequest
{
    [Required(ErrorMessage = "Benutzername ist erforderlich")]
    public string Benutzername { get; set; }

    [Required(ErrorMessage = "Passwort ist erforderlich")]
    public string Passwort { get; set; }

    [Required(ErrorMessage = "Conrifmration ist erforderlich")]
    [MatchProperty("Passwort", ErrorMessage = "Die Passwoerter stimmen nicht überein")]
    public string Confirmation { get; set; }
}