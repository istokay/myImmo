using Microsoft.AspNetCore.Mvc;
using MyImmo.Api.Dtos;
using MyImmo.App.Exceptions;
using MyImmo.App.Services;
using MyImmo.Domain.Dtos;

[ApiController]
[Route("api/[contoller]")]
public class IncomeController(IRealEstateService realEstateService) : ControllerBase
{
    [HttpPost("{realEstateId}/income")]
    public async Task<ActionResult<RealEstateResponseDto>> CreateRealEstateIncome(int realEstateId, [FromBody] IncomePost incomePost)
    {
        try
        {
            var response = await realEstateService.CreateIncome(realEstateId, incomePost);
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

    [HttpGet("{id}")]
    public async Task<ActionResult<RealEstateResponseDto>> GetRealEstateIncomes(int id)
    {
        try
        {
            var response = await realEstateService.GetImcomes(id);
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
    [HttpPut("{realEstateId}/{incomeId}")]
    public async Task<ActionResult<Income>> UpdateRealEstateIncome(int realEstateId, int incomeId, [FromBody] IncomePost income)
    {
        try
        {
            var result = await realEstateService.UpdateIncome(realEstateId, incomeId, income);
            return Ok(result);
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