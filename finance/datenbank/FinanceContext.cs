using Classes;
using Microsoft.EntityFrameworkCore;

namespace finance.Models;
    // Klasse
public class FinanceContext : DbContext
{   // Konstruktor
    public FinanceContext(DbContextOptions<FinanceContext> options)
        : base(options)
    {}
    //  DbSet = Klasse aus entityFramework = Eine Tabelle in der DB.
    public DbSet<Benutzer> Benutzer { get; set; }
    public DbSet<Transaktion> Transaktionen { get; set; }
}