

using System.ComponentModel.DataAnnotations;
using finance.Helper;

namespace Classes;

public class LoginRequest
{
    [Required(ErrorMessage = "Benutzername ist erforderlich")]
    public string Benutzername { get; set; }

    [Required(ErrorMessage = "Passwort ist erforderlich")]
    public string Passwort { get; set; }

}