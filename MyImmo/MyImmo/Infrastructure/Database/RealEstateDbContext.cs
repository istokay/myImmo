using Microsoft.EntityFrameworkCore;
using MyImmo.Domain.Entities;

namespace MyImmo.Domain.Infrastructure.Database;

public class RealEstateDbContext : DbContext
{
    public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : base(options)
    {
    }

    public DbSet<RealEstateEntity> RealEstates { get; set; }
}