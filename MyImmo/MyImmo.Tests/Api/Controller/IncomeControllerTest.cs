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
    public async Task CreateRealEstateIncome_should_return_RealEstateDto_if_Exists()
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
}