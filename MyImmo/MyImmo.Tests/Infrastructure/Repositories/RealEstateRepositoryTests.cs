using Microsoft.EntityFrameworkCore;
using MyImmo.Domain.Dtos;
using MyImmo.Domain.Infrastructure.Database;
using MyImmo.Infrastructure.Repositories;
using Xunit;

namespace MyImmo.Tests.Infrastructure.Repositories;

public class RealEstateRepositoryTests
{
    [Fact]
    public void CreateRealEstate_ProcessNullIncome()
    {
        using var dbContext = CreateDbContext();
        var repository = new RealEstateRepository(dbContext);
        var realEstate = new RealEstatePost
        {
            Name = "Immo1",
            Incomes = null
        };

        var created = repository.CreateRealEstate(realEstate);

        Assert.Equal("Immo1", created.Name);
        Assert.Empty(created.Incomes!);
    }
    [Fact]
    public void CreateRealEstate_ProcessIncome()
    {
        using var dbContext = CreateDbContext();
        var repository = new RealEstateRepository(dbContext);
        var realEstate = CreateRealEstatePost();

        var created = repository.CreateRealEstate(realEstate);

        Assert.Equal("Immo2", created.Name);
        Assert.Equal(2, created.Incomes.Count);


        Assert.Contains(created.Incomes, i =>
            i.Name == "income3" &&
            i.Amount == 28 &&
            i.IncomeCategory == IncomeCategory.OneTimePayment
        );

        Assert.Contains(created.Incomes, i =>
            i.Name == "income2" &&
            i.Amount == 9 &&
            i.IncomeCategory == IncomeCategory.OneTimePayment
        );
    }

    [Fact]
    public void UpateRealEstate()
    {
        using var dbContext = CreateDbContext();
        var repository = new RealEstateRepository(dbContext);
        var created = repository.CreateRealEstate(new RealEstatePost
        {
            Name = "Immo2",
            Incomes = new List<IncomePost>
            {
                new IncomePost
                {
                    Name = "income before",
                    Amount = 334,
                    IncomeCategory = IncomeCategory.OneTimePayment
                }
            }
        });

        var update = new RealEstatePost
        {
            Name = "Immo2 updated",
            Incomes = new List<IncomePost>
            {
                new IncomePost
                {
                    Name = "income after",
                    Amount = 455,
                    IncomeCategory = IncomeCategory.MonthlyPayment
                }
            }
        };

        var updated = repository.UpdateRealEstate(created.Id, update);

        Assert.Equal("Immo2 updated", updated!.Name);
        Assert.Single(updated.Incomes);

        Assert.Contains(updated.Incomes, i =>
            i.Name == "income after" &&
            i.Amount == 455 &&
            i.IncomeCategory == IncomeCategory.MonthlyPayment
        );
    }

    [Fact]
    public void UpateRealEstate_MultipleIncomes()
    {
        using var dbContext = CreateDbContext();
        var repository = new RealEstateRepository(dbContext);
        var created = repository.CreateRealEstate(CreateRealEstatePost());

        var update = CreateUpdateRealEstatePost();

        var updated = repository.UpdateRealEstate(created.Id, update);

        Assert.Equal("Immo2 after", updated!.Name);
        Assert.Equal(2, updated.Incomes.Count);

        Assert.Contains(updated.Incomes, i =>
            i.Name == "income3 after" &&
            i.Amount == 64 &&
            i.IncomeCategory == IncomeCategory.MonthlyPayment
        );

        Assert.Contains(updated.Incomes, i =>
            i.Name == "income2 after" &&
            i.Amount == 455 &&
            i.IncomeCategory == IncomeCategory.MonthlyPayment
        );
    }

    [Fact]
    public void DeleteRealEstate()
    {
        using var dbContext = CreateDbContext();

        var repository = new RealEstateRepository(dbContext);

        var created = repository.CreateRealEstate(new RealEstatePost
        {
            Name = "Immo1"
        });

        repository.DeleteRealEstate(created.Id);

        var persisted = dbContext.RealEstates.ToList();
        Assert.Empty(persisted);
    }

    private static RealEstateDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<RealEstateDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new RealEstateDbContext(options);
    }

    private static RealEstatePost CreateRealEstatePost()
    {
        return new RealEstatePost
        {
            Name = "Immo2",
            Incomes = new List<IncomePost>
            {
                new()
                {
                    Name = "income3",
                    Amount = 28,
                    IncomeCategory = IncomeCategory.OneTimePayment
                },
                new()
                {
                    Name = "income2",
                    Amount = 9,
                    IncomeCategory = IncomeCategory.OneTimePayment
                }
            }
        };
    }
    private static RealEstatePost CreateUpdateRealEstatePost()
    {
        return new RealEstatePost
        {
            Name = "Immo2 after",
            Incomes = new List<IncomePost>
            {
                new()
                {
                    Name = "income3 after",
                    Amount = 64,
                    IncomeCategory = IncomeCategory.MonthlyPayment
                },
                new()
                {
                    Name = "income2 after",
                    Amount = 455,
                    IncomeCategory = IncomeCategory.MonthlyPayment
                }
            }
        };
    }
}