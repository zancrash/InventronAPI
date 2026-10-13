using InventronAPI.Data;
using InventronAPI.Services.Implementations;
using InventronAPI.Services.Interfaces;
using InventronAPI.Services.Utils;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => 
	options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ServiceMapper).Assembly));

builder.Services.AddScoped<IProductsService, ProductsService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
