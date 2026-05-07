using Microsoft.AspNetCore.Mvc;
using Moq;
using MyImmo.Api.Controller;
using MyImmo.App.Exceptions;
using MyImmo.App.Services;
using MyImmo.Domain.Dtos;
using Xunit;

public class RealEstateControllerTest
{
    [Fact]
    public async Task CreateRealEstate_returns_RealEstate()
    {
        var realEstateServiceMock = new Mock<IRealEstateService>();

        var realEstatePost = new RealEstatePost { Name = "realEstate1" };
        realEstateServiceMock.Setup(x => x.CreateRealEstate(realEstatePost))
        .Returns(new RealEstate
        {
            Id = 1,
            Name = realEstatePost.Name
        });

        var result = new RealEstateController(realEstateServiceMock.Object)
        .CreateRealEstate(realEstatePost);

        Assert.IsType<ActionResult<RealEstate>>(result);

        var resultValue = Assert.IsType<RealEstate>(((OkObjectResult)result.Result!).Value);
        Assert.Equal(resultValue.Name, realEstatePost.Name);
        Assert.Equal(1, resultValue.Id);
    }

    [Fact]
    public async Task GetAllRealEstates_returns_RealEstates_if_exist()
    {
        var realEstateServiceMock = new Mock<IRealEstateService>();

        var reList = new List<RealEstate>
        {
            new RealEstate
            {
                Id = 1,
                Name = "re1"
            }
        };
        realEstateServiceMock.Setup(x => x.GetAllRealEstates())
       .Returns(reList);

        var result = new RealEstateController(realEstateServiceMock.Object)
        .GetAllRealEstates();

        Assert.IsType<ActionResult<IReadOnlyCollection<RealEstate>>>(result);

        var resultValue = Assert.IsType<IReadOnlyCollection<RealEstate>>(((OkObjectResult)result.Result!).Value, exactMatch: false);

        foreach (var re in resultValue)
        {
            Assert.Equal("re1", re.Name);
            Assert.Equal(1, re.Id);
        }
    }

    [Fact]
    public async Task GetAllRealEstates_returns_NotFound_if_not_exist()
    {
        var realEstateServiceMock = new Mock<IRealEstateService>();

        realEstateServiceMock.Setup(x => x.GetAllRealEstates())
       .Returns([]);

        var result = new RealEstateController(realEstateServiceMock.Object)
        .GetAllRealEstates();

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task DeleteRealEstateById_returns_Ok_if_exist()
    {
        var realEstateServiceMock = new Mock<IRealEstateService>();

        realEstateServiceMock.Setup(x => x.DeleteRealEstate(1));

        var result = new RealEstateController(realEstateServiceMock.Object)
        .DeleteRealEstateById(1);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task DeleteRealEstateById_returns_NotFound_if_not_exist()
    {
        var realEstateServiceMock = new Mock<IRealEstateService>();

        realEstateServiceMock.Setup(x => x.DeleteRealEstate(2))
        .Throws(new EntityNotFoundException("2"));

        var result = new RealEstateController(realEstateServiceMock.Object)
        .DeleteRealEstateById(2);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task UpdateRealEstate_returns_Ok_if_exist()
    {
        var realEstateServiceMock = new Mock<IRealEstateService>();

        var realEstatePost = new RealEstatePost { Name = "realEstate3" };
        var realEstateId = 1;

        realEstateServiceMock.Setup(x => x.UpdateRealEstate(realEstateId, realEstatePost))
        .Returns(new RealEstate
        {
            Id = realEstateId,
            Name = realEstatePost.Name
        });

        var result = new RealEstateController(realEstateServiceMock.Object)
        .UpdateRealEstate(realEstateId, realEstatePost);

        var actionResult = Assert.IsType<ActionResult<RealEstate>>(result);

        var resultValue = Assert.IsType<RealEstate>((actionResult!).Value);
        Assert.Equal(realEstateId, resultValue.Id);
        Assert.Equal("realEstate3", resultValue.Name);
    }

    [Fact]
    public async Task UpdateRealEstate_returns_NotFound_if_not_exist()
    {
        var realEstateServiceMock = new Mock<IRealEstateService>();

        var realEstatePost = new RealEstatePost { Name = "realEstate4" };

        realEstateServiceMock.Setup(x => x.UpdateRealEstate(2, realEstatePost))
        .Throws(new EntityNotFoundException("2"));

        var result = new RealEstateController(realEstateServiceMock.Object)
        .UpdateRealEstate(2, realEstatePost);

        Assert.IsType<NotFoundResult>(result.Result);
    }
}