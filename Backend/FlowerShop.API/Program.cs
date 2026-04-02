using Microsoft.EntityFrameworkCore;
using FlowerShop.API.Data;
using FlowerShop.API.Repositories;
using FlowerShop.API.Services;
using FlowerShop.API.Mapping;

var builder = WebApplication.CreateBuilder(args);

// 1. Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 3. Register Repositories and Services (Dependency Injection)
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

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

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAnyOrigin");

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
        var errorResponse = System.Text.Json.JsonSerializer.Serialize(new { 
            message = "Internal Server Error. Please contact admin.",
            error = ex.Message 
        });
        await context.Response.WriteAsync(errorResponse);
    }
});

app.MapControllers();

app.Run();
