using MovieReviewApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Konfiguracja bazy danych
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dodanie usług Identity z obsługą ról
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true; // Wymagana weryfikacja konta
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Rejestracja usługi OmdbService
builder.Services.AddHttpClient<OmdbService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["OmdbApi:BaseUrl"]);
});

builder.Services.AddTransient<IEmailSender, FakeEmailSender>();

// Konfiguracja Identity cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// Dodanie Razor Pages
builder.Services.AddRazorPages();

// Dodanie kontrolerów
builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Middleware dla uwierzytelniania i autoryzacji
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages(); // Mapowanie Razor Pages
app.MapControllers(); // Mapowanie kontrolerów

app.Run();
