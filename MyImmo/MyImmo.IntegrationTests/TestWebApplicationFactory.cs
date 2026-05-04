using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyImmo.App.Interfaces;
using MyImmo.App.Services;
using MyImmo.Domain.Infrastructure.Database;
using MyImmo.Infrastructure.Repositories;


namespace MyImmo.IntegrationTests;

/// <summary>
/// TestWebApplicationFactory for MyImmo
/// </summary>
public class TestWebApplicationFactory : WebApplicationFactory<Program>
{

    private SqliteConnection _connection = default!;

    /// <summary>
    /// Configure the WebHost for the IntegrationTests
    /// </summary>
    /// <param name="builder"></param>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddScoped<IRealEstateService, RealEstateService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IRealEstateRepository, RealEstateRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();

            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<RealEstateDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddDbContext<RealEstateDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<RealEstateDbContext>();
            db.Database.EnsureCreated();
        });
    }
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
            _connection.Dispose();
    }
}
