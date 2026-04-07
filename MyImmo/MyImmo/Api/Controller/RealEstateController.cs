using Microsoft.AspNetCore.Mvc;
using MyImmo.Api.Dtos;
using MyImmo.App.Services;

namespace MyImmo.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class RealEstateController(IRealEstateService realEstateService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<RealEstateResponseDto>> GetRealEstate(RealEstateRequestDto request)
    {
        var response = await realEstateService.GetRealEstate(request.Id);
        return Ok(response);
    }
}