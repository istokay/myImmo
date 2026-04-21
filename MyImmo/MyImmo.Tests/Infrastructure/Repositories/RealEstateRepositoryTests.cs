using Microsoft.EntityFrameworkCore;
using MyImmo.App.Dtos;
using MyImmo.Domain.Infrastructure.Database;
using MyImmo.Infrastructure.Repositories;
using Xunit;

namespace MyImmo.Tests.Infrastructure.Repositories;

public class RealEstateRepositoryTests
{
    [Fact]
    public async Task CreateRealEstate_PersistsRealEstateAndMappedIncomes()
    {
        await using var dbContext = CreateDbContext();
        var repository = new RealEstateRepository(dbContext);
        var realEstate = new RealEstatePost
        {
            Name = "Immo1",
            Incomes =
            [
                new IncomePost { Amount = 1200m, IncomeCategory = IncomeCategory.MonthlyPayment },
                new IncomePost { Amount = 5000m, IncomeCategory = IncomeCategory.OneTimePayment }
            ]
        };

        var created = await repository.CreateRealEstate(realEstate);

        var persisted = await dbContext.RealEstates
            .Include(entity => entity.Incomes)
            .SingleAsync();

        Assert.NotNull(persisted.Incomes);
        Assert.Collection(
            persisted.Incomes!,
            income =>
            {
                Assert.Equal(1200m, income.Amount);
                Assert.Equal(IncomeCategory.MonthlyPayment, income.Category);
            },
            income =>
            {
                Assert.Equal(5000m, income.Amount);
                Assert.Equal(IncomeCategory.OneTimePayment, income.Category);
            });
    }

    [Fact]
    public async Task CreateRealEstate_AllowsMissingIncomes()
    {
        await using var dbContext = CreateDbContext();
        var repository = new RealEstateRepository(dbContext);
        var realEstate = new RealEstatePost
        {
            Name = "Immo",
            Incomes = null
        };

        await repository.CreateRealEstate(realEstate);

        var persisted = await dbContext.RealEstates
            .Include(entity => entity.Incomes)
            .SingleAsync();

        Assert.NotNull(persisted.Incomes);
        Assert.Empty(persisted.Incomes!);
    }

    private static RealEstateDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<RealEstateDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new RealEstateDbContext(options);
    }
}