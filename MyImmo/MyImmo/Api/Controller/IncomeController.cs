using Microsoft.AspNetCore.Mvc;
using MyImmo.Api.Dtos;
using MyImmo.App.Exceptions;
using MyImmo.App.Services;
using MyImmo.Domain.Dtos;

namespace MyImmo.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class IncomeController(IIncomeService incomeService) : ControllerBase
{
    [HttpPost("{realEstateId}/income")]
    public async Task<ActionResult<Income>> CreateRealEstateIncome(int realEstateId, [FromBody] IncomePost incomePost)
    {
        try
        {
            var response = await incomeService.CreateIncome(realEstateId, incomePost);
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

    [HttpGet("{realEstateId}")]
    public async Task<ActionResult<IReadOnlyCollection<Income>>> GetRealEstateIncomes(int realEstateId)
    {
        try
        {
            var response = await incomeService.GetImcomes(realEstateId);
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
            await incomeService.DeleteRealEstateIncome(realEstateId, incomeId);
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
            var result = await incomeService.UpdateIncome(realEstateId, incomeId, income);
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