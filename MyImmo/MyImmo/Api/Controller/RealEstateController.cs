using Microsoft.AspNetCore.Mvc;
using MyImmo.Api.Dtos;
using MyImmo.Domain.Dtos;
using MyImmo.App.Exceptions;
using MyImmo.App.Services;

namespace MyImmo.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class RealEstateController(IRealEstateService realEstateService) : ControllerBase
{
    [HttpPost]
    public ActionResult<RealEstate> CreateRealEstate([FromBody] RealEstatePost realEstate)
    {
        var response = realEstateService.CreateRealEstate(realEstate);
        return Ok(response);
    }

    [HttpGet]
    public ActionResult<IReadOnlyCollection<RealEstate>> GetAllRealEstates()
    {
        var response = realEstateService.GetAllRealEstates();

        if (response != null && response.Count > 0)
            return Ok(response);
        else
            return NotFound();
    }

    [HttpDelete("{realEstateId}")]
    public ActionResult DeleteRealEstateById(int realEstateId)
    {
        try
        {
            realEstateService.DeleteRealEstate(realEstateId);
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
    [HttpPut("{id}")]
    public ActionResult<RealEstate> UpdateRealEstate(int id, [FromBody] RealEstatePost realEstate)
    {
        try
        {
            var result = realEstateService.UpdateRealEstate(id, realEstate);
            return result;
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