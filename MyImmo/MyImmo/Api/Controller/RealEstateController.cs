using Microsoft.AspNetCore.Mvc;
using MyImmo.Api.Dtos;
using MyImmo.App.Dtos;
using MyImmo.App.Exceptions;
using MyImmo.App.Services;

namespace MyImmo.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class RealEstateController(IRealEstateService realEstateService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<RealEstateResponse>> CreateRealEstate(RealEstatePost realEstate)
    {
        var response = await realEstateService.CreateRealEstate(realEstate);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<AllRealEstatesResponseDto>> GetAllRealEstates()
    {
        var response = await realEstateService.GetAllRealEstates();

        if (response != null && response.Count > 0)
            return Ok(response);
        else
            return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RealEstateResponse>> GetRealEstateById(int id)
    {
        try
        {
            var response = await realEstateService.GetRealEstate(id);
            return Ok(response);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpDelete("{realEstateId}/{incomeId}")]
    public async Task<ActionResult> DeleteIncomeById(int realEstateId, int incomeId)
    {
        try
        {
            await realEstateService.DeleteRealEstateIncome(realEstateId, incomeId);
            return Ok();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}