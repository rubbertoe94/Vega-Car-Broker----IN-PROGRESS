using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using vega.Pages.Persistence;
using vega.Pages.Persistence.Interfaces;
using vega.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services to the container.
builder.Services.AddDbContext<VegaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllersWithViews();

// builder.Services.AddControllers().AddJsonOptions(x =>
//     x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://localhost:44494", "https://localhost:7220", "http://localhost:5008")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});



var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();
app.UseStaticFiles();

// Place Swagger before UseRouting.
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

app.UseRouting();

app.UseAuthorization(); // Add this if you have [Authorize] attributes.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

// The Swagger UI is now configured before MapFallbackToFile
// so it shouldn't be caught by the fallback route.
app.MapFallbackToFile("index.html");

app.Run();
