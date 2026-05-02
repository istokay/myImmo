using Microsoft.AspNetCore.Mvc;
using Moq;
using MyImmo.Api.Controller;
using MyImmo.Api.Dtos;
using MyImmo.App.Exceptions;
using MyImmo.App.Services;
using MyImmo.Domain.Dtos;
using Xunit;

namespace MyImmo.Tests.Api.Controller;


public class IncomeControllerTest
{
    [Fact]
    public async Task CreateRealEstateIncome_should_return_Income_if_Exists()
    {
        var incomeServiceMock = new Mock<IIncomeService>();

        var realEstateId = 10;

        var incomePost = new IncomePost
        {
            Name = "income1",
            Amount = 200,
            IncomeCategory = IncomeCategory.AnnualPayment
        };

        incomeServiceMock.Setup(x => x.CreateIncome(realEstateId, incomePost))
            .ReturnsAsync(
                new Income
                {
                    Id = 0,
                    Name = incomePost.Name,
                    Amount = incomePost.Amount,
                    IncomeCategory = incomePost.IncomeCategory,
                    RealEstateId = realEstateId
                }
            );

        var controller = new IncomeController(incomeServiceMock.Object);

        var result = await controller.CreateRealEstateIncome(realEstateId, incomePost);

        var okResult = Assert.IsType<ActionResult<Income>>(result);
        var income = Assert.IsType<Income>(((OkObjectResult)result!.Result!).Value);

        Assert.Equal(0, income.Id);
        Assert.Equal(200, income.Amount);
        Assert.Equal(IncomeCategory.AnnualPayment, income.IncomeCategory);
        Assert.Equal(10, income.RealEstateId);
    }
    [Fact]
    public async Task CreateRealEstateIncome_should_return_NotFound_if_not_exists()
    {
        var incomeServiceMock = new Mock<IIncomeService>();

        var realEstateId = 330;

        var incomePost = new IncomePost
        {
            Name = "income1",
            Amount = 200,
            IncomeCategory = IncomeCategory.AnnualPayment
        };

        incomeServiceMock.Setup(x => x.CreateIncome(realEstateId, incomePost))
            .ThrowsAsync(new EntityNotFoundException(realEstateId.ToString()));

        var controller = new IncomeController(incomeServiceMock.Object);

        var result = await controller.CreateRealEstateIncome(realEstateId, incomePost);

        var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);

        Assert.Equal(404, notFoundResult.StatusCode);
    }
    [Fact]
    public async Task GetRealEstateIncome_should_return_Income_if_Exists()
    {
        var incomeServiceMock = new Mock<IIncomeService>();

        var realEstateId = 11;

        var incomePost = new IncomePost
        {
            Name = "income2",
            Amount = 44,
            IncomeCategory = IncomeCategory.MonthlyPayment
        };

        incomeServiceMock.Setup(x => x.GetImcomes(realEstateId))
            .ReturnsAsync(
                (IReadOnlyCollection<Income>)
                new List<Income>
                {
                    new Income
                    {
                        Id = 3,
                        Name = incomePost.Name,
                        Amount = incomePost.Amount,
                        IncomeCategory = incomePost.IncomeCategory,
                        RealEstateId = realEstateId
                    }
                }
            );

        var controller = new IncomeController(incomeServiceMock.Object);

        var result = await controller.GetRealEstateIncomes(realEstateId);

        var okResult = Assert.IsType<ActionResult<IReadOnlyCollection<Income>>>(result);
        var incomes = Assert.IsType<IReadOnlyCollection<Income>>(((OkObjectResult)result!.Result!).Value, exactMatch: false);

        foreach (var income in incomes)
        {
            Assert.Equal(3, income.Id);
            Assert.Equal(44, income.Amount);
            Assert.Equal(IncomeCategory.MonthlyPayment, income.IncomeCategory);
            Assert.Equal(11, income.RealEstateId);
        }
    }
}