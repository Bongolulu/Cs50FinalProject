using System.Security.Cryptography;
using System.Text;

namespace finance.Models;

public class Benutzer
{
    public string Name { get;  set; }
    public string PasswortHash { get;  set; }
    public decimal Bargeld { get;  set; }
    public Guid BenutzerId { get;  set; }
    public List<Transaktion> _allTransaktions = new List<Transaktion>();
    
    // Standardkonstruktor ohne Parameter für Entity Framework Core
    public Benutzer()
    {
    }
    
    // Konstruktor
    public Benutzer(string name, string passwort)
    {
        
        Name = name;
        PasswortHash = HashPasswort (passwort);
        Bargeld = 10000;    //festgelegt
        BenutzerId = Guid.NewGuid();
    }
    
    //Methothen:
    //Passwort hashen:
    private string HashPasswort(string passwort)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(passwort));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2")); // Hexadezimale Darstellung
            }
            return builder.ToString();
        }
    }
    // Methode zum Überprüfen des Passworts
    public bool PasswortPrüfen(string eingabePasswort)
    {
        string eingabePasswortHash = HashPasswort(eingabePasswort);
        return eingabePasswortHash == PasswortHash;
    }
}
