using System.Configuration;
using Classes;
using finance.Helper;
using finance.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

// Erstelle einen builder (erstellt Webapplikationen). Mit diesem Objekt erstellt man Webapplikationen
var builder = WebApplication.CreateBuilder(args);

// Web- API (ein oder mehrere Controller). Objekt builder benutze ich, um Bausteine hinzuzufügen,welche ich für mein Programm
//builder.Services.AddControllers();      // benötige. Hier Baustein Controller.

// DB. Baustein DB wird dem builder hinzugefügt.
builder.Services.AddDbContext<FinanceContext>(opt => 
    opt.UseNpgsql("Server=192.168.2.2;Port=5432;Database=finance;Username=myUser;Password=Password12!"));

//Authentifizierung mit Token (bedeutet nur 1x einloggen und mit jeder weiteren Abfrage wird der Token geschickt.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();

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
    return Results.Ok(token);
}).Validate<LoginRequest>();

/*
// Route buy post
app.MapPost("/buy", async (Todo todo, FinanceContext db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/buy/{todo.Id}", todo);
});

// Route history get
app.MapGet("/history", async (FinanceContext db) =>
    "kommt noch");

// Route index get
app.MapGet("/index", async (FinanceContext db) =>
    "kommt noch");


// Route quote get
app.MapGet("/quote", async (FinanceContext db) =>
    "kommt noch");

// Route quote post
app.MapPost("/quote", async (Todo todo, FinanceContext db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/quote/{todo.Id}", todo);
});



// Route sell post
app.MapPost("/sell", async (Todo todo, FinanceContext db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/sell/{todo.Id}", todo);
});



    //await db.Benutzer.ToListAsync());
/*
app.MapGet("/todoitems/complete", async (FinanceContext db) =>
    await db.Todos.Where(t => t.IsComplete).ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, FinanceContext db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.MapPost("/todoitems", async (Todo todo, FinanceContext db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", todo);
});

app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, FinanceContext db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, FinanceContext db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});
*/
app.Run();