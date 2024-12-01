using IntegrationV3R_PortailFournisseur.Data.Models;
using IntegrationV3R_PortailFournisseur.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Blazored.SessionStorage;
using static IntegrationV3R_PortailFournisseur.Shared.ComposantsFormulaire.NavbarFormulaire;
using IntegrationV3R_PortailFournisseur.Shared;
using IntegrationV3R_PortailFournisseur.Data.Services.Scoped;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services � l'application.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredSessionStorage();


// Configuration des sessions
builder.Services.AddHttpContextAccessor(); // N�cessaire pour acc�der � HttpContext
builder.Services.AddSession(); // Ajouter la gestion des sessions

// Configuration de la base de donn�es avec MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 18))));


builder.Services.AddScoped<SharedDataService>();
builder.Services.AddScoped<SendMailService>();

builder.Services.AddHttpClient<DonneesQuebecService>(client =>
{
    client.BaseAddress = new Uri("https://www.donneesquebec.ca/");
});

builder.Services.AddScoped<SingletonFormulaire>();

var app = builder.Build();

// Configure le pipeline HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Utiliser les sessions dans l'application
app.UseSession(); // Activer les sessions avant de les utiliser dans les composants

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
