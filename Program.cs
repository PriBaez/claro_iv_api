using System.Text.Json.Serialization;
using CLARO_IV_API.Interfaces.Categories;
using CLARO_IV_API.Interfaces.Products;
using CLARO_IV_API.Models;
using CLARO_IV_API.Repositories;
using CLARO_IV_API.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductIvContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("dbconnection")));

//Services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
//CORS
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowCredentials().AllowAnyHeader().WithExposedHeaders("Content-Disposition");;
}));

//Serilog
builder.Host.UseSerilog((context, configuration) => {
    configuration.ReadFrom.Configuration(context.Configuration);
});
    
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseCors("corsapp");
app.UseHttpsRedirection();

// Mapear rutas a controladores
app.MapControllers();

app.Run();