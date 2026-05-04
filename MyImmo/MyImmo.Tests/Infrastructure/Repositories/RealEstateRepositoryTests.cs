using Microsoft.EntityFrameworkCore;
using MyImmo.Domain.Dtos;
using MyImmo.Domain.Infrastructure.Database;
using MyImmo.Infrastructure.Repositories;
using Xunit;

namespace MyImmo.Tests.Infrastructure.Repositories;

public class RealEstateRepositoryTests
{
    [Fact]
    public async Task CreateRealEstate()
    {
        await using var dbContext = CreateDbContext();
        var repository = new RealEstateRepository(dbContext);
        var realEstate = new RealEstatePost
        {
            Name = "Immo1",
        };

        var created = await repository.CreateRealEstate(realEstate);

        Assert.Equal("Immo1", created.Name);
    }

    [Fact]
    public async Task UpateRealEstate()
    {
        await using var dbContext = CreateDbContext();
        var repository = new RealEstateRepository(dbContext);
        var created = await repository.CreateRealEstate(new RealEstatePost
        {
            Name = "Immo2",
        });

        var update = new RealEstatePost
        {
            Name = "Immo2 updated",
        };

        var updated = await repository.UpdateRealEstate(created.Id, update);

        Assert.Equal("Immo2 updated", updated!.Name);
    }

    [Fact]
    public async Task DeleteRealEstate()
    {
        await using var dbContext = CreateDbContext();

        var repository = new RealEstateRepository(dbContext);

        var created = await repository.CreateRealEstate(new RealEstatePost
        {
            Name = "Immo1"
        });

        await repository.DeleteRealEstate(created.Id);

        var persisted = await dbContext.RealEstates.ToListAsync();
        Assert.Empty(persisted);
    }

    private static RealEstateDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<RealEstateDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new RealEstateDbContext(options);
    }
}