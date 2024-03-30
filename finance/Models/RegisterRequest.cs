

using System.ComponentModel.DataAnnotations;

namespace Classes;

public class RegisterRequest
{
    [Required(ErrorMessage = "Benutzername ist erforderlich")]
    public string Benutzername { get; set; }

    [Required(ErrorMessage = "Passwort ist erforderlich")]
    public string Passwort { get; set; }

    [Required(ErrorMessage = "Bestätigung ist erforderlich")]
    public string Confirmation { get; set; }
}