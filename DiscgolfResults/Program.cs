using DiscgolfResults.Extensions;
using Microsoft.OpenApi.Models;
using Results.Domain;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services    
    .AddResultBackend(builder.Configuration)
    .AddTranslators();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseStaticFiles();
app.UseRouting();

app.UseLoggingMiddleware();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
