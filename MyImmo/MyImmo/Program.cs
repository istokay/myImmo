using Microsoft.EntityFrameworkCore;
using MyImmo.App.Services;
using MyImmo.Domain.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddScoped<IRealEstateService, RealEstateService>();
builder.Services.AddOpenApi();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<RealEstateDbContext>(options =>
    options.UseSqlite(connString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.Run();

