using MyImmo.Domain.Dtos;
using MyImmo.App.Exceptions;
using MyImmo.App.Interfaces;

namespace MyImmo.App.Services;

public class IncomeService(IIncomeRepository realEstateRepository) : IIncomeService
{
    public async Task<IReadOnlyCollection<Income>> GetImcomes(int realEstateId)
    {
        var result = await realEstateRepository.GetImcomes(realEstateId);

        if (result == null)
            throw new EntityNotFoundException(realEstateId.ToString());

        return result;
    }

    public async Task<Income> CreateIncome(int realEstateId, IncomePost incomePost)
    {
        var result = await realEstateRepository.CreateIncome(realEstateId, incomePost);

        if (result == null)
            throw new EntityNotFoundException(realEstateId.ToString());

        return result;
    }

    public async Task DeleteRealEstateIncome(int realEstateId, int incomeId)
    {
        var isDeleted = await realEstateRepository.DeleteRealEstateIncome(realEstateId, incomeId);
        if (!isDeleted)
            throw new EntityNotFoundException(realEstateId.ToString());
    }

    public async Task<Income> UpdateIncome(int realEstateId, int incomeId, IncomePost incomePost)
    {
        var income = await realEstateRepository.UpdateIncome(realEstateId, incomeId, incomePost);

        if (income == null)
        {
            throw new EntityNotFoundException(incomeId.ToString());
        }

        return income;
    }
}