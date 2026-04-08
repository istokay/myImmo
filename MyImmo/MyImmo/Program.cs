using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using MyImmo.App.Interfaces;
using MyImmo.App.Services;
using MyImmo.Domain.Infrastructure.Database;
using MyImmo.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MyImmo API",
        Version = "v1",
        Description = "API documentation for MyImmo real estate endpoints."
    });
});
builder.Services.AddScoped<IRealEstateService, RealEstateService>();
builder.Services.AddScoped<IRealEstateRepository, RealEstateRepository>();
builder.Services.AddOpenApi();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<RealEstateDbContext>(options =>
    options.UseSqlite(connString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

