using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;

var builder = WebApplication.CreateBuilder(args);

// Adaugă serviciul de context pentru baza de date
builder.Services.AddDbContext<AplicatieStudentiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexiuneDefault")));

// Adaugă serviciile necesare pentru Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Dacă aplicația este în modul de dezvoltare, arată erorile de debug
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Acasa/Eroare");
    app.UseHsts();
}

// Setează rutele pentru aplicație
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Mapează paginile Razor
app.MapRazorPages();

app.Run();

