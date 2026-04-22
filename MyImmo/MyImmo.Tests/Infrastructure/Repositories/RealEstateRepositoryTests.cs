using Microsoft.EntityFrameworkCore;
using MyImmo.Domain.Dtos;
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
        };

        var created = await repository.CreateRealEstate(realEstate);

        var persisted = await dbContext.RealEstates
            .Include(entity => entity.Incomes)
            .SingleAsync();

        Assert.Empty(persisted.Incomes!);
        Assert.Equal("Immo1", persisted.Name);
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