using Microsoft.EntityFrameworkCore;
using FlowerShop.API.Data;
using FlowerShop.API.Repositories;
using FlowerShop.API.Services;
using FlowerShop.API.Mapping;
using FlowerShop.API.Helpers;

var builder = WebApplication.CreateBuilder(args);

// 1. Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 3. Register Repositories and Services (Dependency Injection)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IImageService, ImageService>();

// Configure Cloudinary
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

// 4. Add CORS for Frontend Integration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable Routing
app.UseRouting();

// Use CORS - Move before HttpsRedirection and other middleware
app.UseCors("AllowAnyOrigin");

// app.UseHttpsRedirection(); // Commented out for local development to avoid redirect issues with CORS

app.UseAuthorization();


// Global Exception Handling Enhancement
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var response = new 
        { 
            message = "Internal Server Error. Please contact admin.",
            details = context.Request.Host.Host == "localhost" ? ex.Message : null 
        };
        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
});

app.MapControllers();

app.Run();
