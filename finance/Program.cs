using System.Configuration;
using finance.datenbank;
using finance.Helper;
using finance.Models;
using Infrastructure.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis.Elfie.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

// Erstelle einen builder (erstellt Webapplikationen). Mit diesem Objekt erstellt man Webapplikationen
var builder = WebApplication.CreateBuilder(args);

// Web- API (ein oder mehrere Controller). Objekt builder benutze ich, um Bausteine hinzuzufügen,welche ich für mein Programm
//builder.Services.AddControllers();      // benötige. Hier Baustein Controller.

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

// DB. Baustein DB wird dem builder hinzugefügt.
builder.Services.AddDbContext<FinanceContext>(opt => 
    opt.UseNpgsql("Server=192.168.2.2;Port=5432;Database=finance;Username=myUser;Password=Password12!", o=> o.UseNodaTime()));

//Authentifizierung mit Token (bedeutet nur 1x einloggen und mit jeder weiteren Abfrage wird der Token geschickt.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddAuthorization();

// Danach rufe ich Methode .Build auf um mit den Bausteinen die Webapplikation zusammenzustellen.
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


// Route register post
// 
app.MapPost("/register", async (RegisterRequest registerRequest, FinanceContext db) =>
{
    // Checken, ob der Benutzername schon existiert, falls ja, Fehlermeldung
    if (db.Benutzer.Count(benutzer => benutzer.Name == registerRequest.Benutzername) >0)
        return Results.BadRequest("Der Benutzername existiert bereits.");
    
    // Neuen Benutzer in DB anlegen:
    Benutzer neuerBenutzer = new Benutzer(registerRequest.Benutzername, registerRequest.Passwort);
    db.Benutzer.Add(neuerBenutzer);
    
    //in db speichern:
    await db.SaveChangesAsync();
    
    // Antwort:
    return Results.Created();
}).Validate<RegisterRequest>(); 

// Route login post
app.MapPost("/login", async (LoginRequest loginRequest, FinanceContext db, IJwtProvider jwtProvider) =>
{
    var benutzer = db.Benutzer.FirstOrDefault(benutzer => benutzer.Name == loginRequest.Benutzername);
    

    // checken, ob Benutzername in db vorhanden und wenn ja, ob Passwort stimmt
    if (benutzer is null || !benutzer.PasswortPrüfen(loginRequest.Passwort))
        return Results.Unauthorized();
    
    
    string token = await jwtProvider.GenerateAsync(benutzer);
    return Results.Ok(new LoginResponse(token));
    
}).Validate<LoginRequest>();


// Route quote post
app.MapPost("/quote", async (QuoteRequest quoteRequest, FinanceContext db) =>
{
    // Aktuellen Aktienkurs abfragen:
    AktienKursAbfrager abfrager = new AktienKursAbfrager();
    decimal aktienkurs = abfrager.GetStockQuote(quoteRequest.Symbol);
    if (aktienkurs == 0)
        return Results.NotFound();
    return Results.Ok(aktienkurs);
}).RequireAuthorization().Validate<QuoteRequest>();

// Route buy post
app.MapPost("/buy", async (TradeRequest tradeRequest, FinanceContext db, HttpContext context) =>
{
    // Aktuellen Aktienkurs abfragen:
    AktienKursAbfrager abfrager = new AktienKursAbfrager();
    decimal aktienkurs = abfrager.GetStockQuote(tradeRequest.Symbol);
    if (aktienkurs == 0)
        return Results.NotFound();
    
    // Kaufkosten berechnen:
    decimal kaufkosten = tradeRequest.Anzahl * aktienkurs;
    
    // benutzer aus db holen:
    var benutzer = db.Benutzer.FirstOrDefault(benutzer => benutzer.Name == context.User.Identity.Name);
    
    // Steht noch genug Geld zur Verfügung?
    if (benutzer.Bargeld < kaufkosten)
        return Results.BadRequest("Nicht genügend Geld vorhanden.");
    
    // Wie viel Geld ist vorhanden?
    decimal neuerKontostand = benutzer.Bargeld - kaufkosten;
    
    // Kauf in DB einfügen:
    Transaktion neueTransaktion = new Transaktion(benutzer, tradeRequest.Symbol, tradeRequest.Anzahl, aktienkurs);
    db.Transaktionen.Add(neueTransaktion);
    
    // DB aktualisieren (Bargeld)
    benutzer.Bargeld = neuerKontostand;
    
    //in db speichern:
    await db.SaveChangesAsync();
    
    return Results.Created();
}).RequireAuthorization().Validate<TradeRequest>();

// Route sell post

app.MapPost("/sell", async (TradeRequest tradeRequest, FinanceContext db, HttpContext context) =>
{
    // Aktuellen Aktienkurs abfragen:
    AktienKursAbfrager abfrager = new AktienKursAbfrager();
    decimal aktienkurs = abfrager.GetStockQuote(tradeRequest.Symbol);
    if (aktienkurs == 0)
        return Results.NotFound();
    
    // Verkaufskosten berechnen:
    decimal verkaufserloes = tradeRequest.Anzahl * aktienkurs;
    
    // benutzer aus db holen:
    var benutzer = db.Benutzer.FirstOrDefault(benutzer => benutzer.Name == context.User.Identity.Name);
    
    // Aktuelle Anzahl Aktien aus DB holen.
    var aktienbestand = db.Transaktionen
        .Where(t => t.Benutzer == benutzer && t.Symbol == tradeRequest.Symbol)
        .Sum(t=>t.Anzahl);
    
    // Sind noch genügend Aktien vorhanden?
    
    if (tradeRequest.Anzahl > aktienbestand)
        return Results.BadRequest("Nicht genügend Aktien vorhanden. Du hast "+aktienbestand+".");
    
    // Wie viel Geld ist vorhanden?
    decimal neuerKontostand = benutzer.Bargeld + verkaufserloes;
    
    // Verkauf in DB einfügen:
    Transaktion neueTransaktion = new Transaktion(benutzer, tradeRequest.Symbol, -tradeRequest.Anzahl, aktienkurs);
    db.Transaktionen.Add(neueTransaktion);
    
    // DB aktualisieren (Bargeld)
    benutzer.Bargeld = neuerKontostand;
    
    //in db speichern:
    await db.SaveChangesAsync();
    
    return Results.Created();
}).RequireAuthorization().Validate<TradeRequest>();


// Route history get
app.MapGet("/history", async (FinanceContext db, HttpContext context) =>
{
    // benutzer aus db holen:
    var benutzer = db.Benutzer.FirstOrDefault(benutzer => benutzer.Name == context.User.Identity.Name);
    return db.Transaktionen
        .Where(t => t.Benutzer == benutzer);
});

// Route portfolio get
app.MapGet("/", async (FinanceContext db, HttpContext context) =>
{
    AktienKursAbfrager abfrager = new AktienKursAbfrager();
    // benutzer aus db holen:
    var benutzer = db.Benutzer.FirstOrDefault(benutzer => benutzer.Name == context.User.Identity.Name);
    var bestand = db.Transaktionen
        .Where(t => t.Benutzer == benutzer)
        .GroupBy(t => t.Symbol).AsEnumerable()
        .Select(gruppe => new
        {
            Symbol = gruppe.Key,
            Anzahl = gruppe.Sum((t=>t.Anzahl)),
            Preis = abfrager.GetStockQuote(gruppe.Key),
        });
    return new
    {
        Bargeld = benutzer.Bargeld,
        Bestand = bestand,
        Name = benutzer.Name
    };
    // Gesamtsumme der Aktien:


    // Bargeld:

    // Gesamtsumme Geld
});
app.Run();