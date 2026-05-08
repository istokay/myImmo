using Microsoft.EntityFrameworkCore;
using MyImmo.Domain.Entities;

namespace MyImmo.Domain.Infrastructure.Database;

public class RealEstateDbContext : DbContext
{
    public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : base(options)
    {
    }

    public DbSet<RealEstateEntity> RealEstates { get; set; }
    public DbSet<IncomeEntity> Incomes { get; set; }
    public DbSet<ExpensesEntity> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RealEstateEntity>()
        .HasMany(re => re.Incomes)
        .WithOne(i => i.RealEstate)
        .HasForeignKey(i => i.RealEstateId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RealEstateEntity>()
        .HasMany(re => re.Expenses)
        .WithOne(i => i.RealEstate)
        .HasForeignKey(i => i.RealEstateId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}