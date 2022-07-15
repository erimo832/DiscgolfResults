using Microsoft.OpenApi.Models;
using Results.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddResultBackend(builder.Configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();


/*
 
TODO:
 - Separate view- and backend-models with transformers
 - Start to migrate old pages
 - New
    - Player stats
        - Hcp trend
        - Score trend
        - Best/Worst/avgerage score
    - Course stats
        - Avg score
        - ???
    - Hole stats
        - Avg score per series
 - Move solution file
 - Disable highlighting swagger
 
 */
