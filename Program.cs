using Microsoft.EntityFrameworkCore;
using AplicatieStudenti.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AplicatieStudentiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexiuneDefault")));

builder.Services.AddRazorPages();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Acasa/Eroare");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();

