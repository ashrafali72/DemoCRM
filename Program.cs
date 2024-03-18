using Business;
using DemoCRM.Model;
using DemoCRM.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
var connectionString = builder.Configuration.GetConnectionString("myCon");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString, options => options.EnableRetryOnFailure()));

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddSingleton<MyStateContainer>();
builder.Services.AddSingleton<CommonProcess>();
//builder.Services.AddReverseProxy()
//    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
//app.MapReverseProxy();
app.Run();
