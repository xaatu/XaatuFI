using System;
using CryptoMarketTracker.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register CryptoService
builder.Services.AddHttpClient<CryptoService>(client =>
{
    client.BaseAddress = new Uri("https://api.coingecko.com/api/v3/");
});

// Register NewsService
builder.Services.AddHttpClient<NewsService>(client =>
{
    client.BaseAddress = new Uri("https://min-api.cryptocompare.com/data/v2/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Crypto}/{action=Index}/{id?}");

app.Run();