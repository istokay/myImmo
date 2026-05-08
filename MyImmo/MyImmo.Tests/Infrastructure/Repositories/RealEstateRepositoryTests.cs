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
        Assert.Empty(created.Expenses!);
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
            i.PaymentRange == PaymentRange.OneTimePayment &&
            i.InitialDate == new DateTime(2022, 8, 19)
        );

        Assert.Contains(created.Incomes, i =>
            i.Name == "income2" &&
            i.Amount == 9 &&
            i.PaymentRange == PaymentRange.OneTimePayment &&
            i.InitialDate == new DateTime(2022, 8, 20)
        );

        Assert.Contains(created.Expenses, i =>
            i.Name == "expenses3" &&
            i.Amount == 23 &&
            i.PaymentRange == PaymentRange.AnnualPayment &&
            i.InitialDate == new DateTime(2022, 8, 19)
        );

        Assert.Contains(created.Expenses, i =>
            i.Name == "expenses4" &&
            i.Amount == 98 &&
            i.PaymentRange == PaymentRange.AnnualPayment &&
            i.InitialDate == new DateTime(2022, 8, 20)
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
                    PaymentRange = PaymentRange.OneTimePayment,
                    InitialDate = new DateTime(2026, 2, 23)
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
                    PaymentRange = PaymentRange.MonthlyPayment,
                    InitialDate = new DateTime(2026, 2, 24)
                }
            }
        };

        var updated = repository.UpdateRealEstate(created.Id, update);

        Assert.Equal("Immo2 updated", updated!.Name);
        Assert.Single(updated.Incomes);

        Assert.Contains(updated.Incomes, i =>
            i.Name == "income after" &&
            i.Amount == 455 &&
            i.PaymentRange == PaymentRange.MonthlyPayment &&
            i.InitialDate == new DateTime(2026, 2, 24)
        );
    }

    [Fact]
    public void UpateRealEstate_MultipleIncomesAndExpenses()
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
            i.PaymentRange == PaymentRange.MonthlyPayment &&
            i.InitialDate == new DateTime(2015, 5, 5)
        );

        Assert.Contains(updated.Incomes, i =>
            i.Name == "income2 after" &&
            i.Amount == 455 &&
            i.PaymentRange == PaymentRange.MonthlyPayment &&
            i.InitialDate == new DateTime(2018, 8, 7)
        );

        Assert.Contains(updated.Expenses, i =>
            i.Name == "expenses3 after" &&
            i.Amount == 4 &&
            i.PaymentRange == PaymentRange.OneTimePayment &&
            i.InitialDate == new DateTime(2015, 5, 5)
        );

        Assert.Contains(updated.Expenses, i =>
            i.Name == "expenses4 after" &&
            i.Amount == 4589 &&
            i.PaymentRange == PaymentRange.OneTimePayment &&
            i.InitialDate == new DateTime(2018, 8, 7)
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

    [Fact]
    public void DeleteRealEstateIncomesAndExpenses()
    {
        using var dbContext = CreateDbContext();

        var repository = new RealEstateRepository(dbContext);

        var created = repository.CreateRealEstate(CreateRealEstatePost());

        repository.DeleteRealEstate(created.Id);

        var persisted = dbContext.RealEstates.ToList();
        var persistedIncomes = dbContext.Incomes.ToList();
        var persistedExpenses = dbContext.Expenses.ToList();
        Assert.Empty(persisted);
        Assert.Empty(persistedIncomes);
        Assert.Empty(persistedExpenses);
    }

    [Fact]
    private void GetAllRealEstates_ReturnsNullIfEmpty()
    {
        using var dbContext = CreateDbContext();
        var repository = new RealEstateRepository(dbContext);

        var result = repository.GetAllRealEstates();

        Assert.Empty(result);
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
                    PaymentRange = PaymentRange.OneTimePayment,
                    InitialDate = new DateTime(2022, 8, 19)
                },
                new()
                {
                    Name = "income2",
                    Amount = 9,
                    PaymentRange = PaymentRange.OneTimePayment,
                    InitialDate = new DateTime(2022, 8, 20)
                }
            },
            Expenses = new List<ExpensesPost>
            {
                new()
                {
                    Name = "expenses3",
                    Amount = 23,
                    PaymentRange = PaymentRange.AnnualPayment,
                    InitialDate = new DateTime(2022, 8, 19)
                },
                new()
                {
                    Name = "expenses4",
                    Amount = 98,
                    PaymentRange = PaymentRange.AnnualPayment,
                    InitialDate = new DateTime(2022, 8, 20)
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
                    PaymentRange = PaymentRange.MonthlyPayment,
                    InitialDate = new DateTime(2015, 5, 5)
                },
                new()
                {
                    Name = "income2 after",
                    Amount = 455,
                    PaymentRange = PaymentRange.MonthlyPayment,
                    InitialDate = new DateTime(2018, 8, 7)
                }
            },
            Expenses = new List<ExpensesPost>
            {
                new()
                {
                    Name = "expenses3 after",
                    Amount = 4,
                    PaymentRange = PaymentRange.OneTimePayment,
                    InitialDate = new DateTime(2015, 5, 5)
                },
                new()
                {
                    Name = "expenses4 after",
                    Amount = 4589,
                    PaymentRange = PaymentRange.OneTimePayment,
                    InitialDate = new DateTime(2018, 8, 7)
                }
            }
        };
    }
}