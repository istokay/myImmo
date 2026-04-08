using Microsoft.AspNetCore.Mvc;
using MyImmo.Api.Dtos;
using MyImmo.App.Dtos;
using MyImmo.App.Services;

namespace MyImmo.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class RealEstateController(IRealEstateService realEstateService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<SingleRealEstateResponseDto>> CreateRealEstate(RealEstate realEstate)
    {
        var response = await realEstateService.CreateRealEstate(realEstate);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<AllRealEstatesResponseDto>> GetAllRealEstates()
    {
        var response = await realEstateService.GetAllRealEstates();
        
        if(response != null && response.Count > 0)
            return Ok(response);
        else 
            return NotFound();
    }
}