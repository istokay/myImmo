using MyImmo.Domain.Dtos;
using MyImmo.App.Exceptions;
using MyImmo.App.Interfaces;

namespace MyImmo.App.Services;

public class RealEstateService(IRealEstateRepository realEstateRepository) : IRealEstateService
{
    public async Task<RealEstate> CreateRealEstate(RealEstatePost realEstate)
    {
        return await realEstateRepository.CreateRealEstate(realEstate);
    }

    public async Task DeleteRealEstateIncome(int realEstateId, int incomeId)
    {
        var isDeleted = await realEstateRepository.DeleteRealEstateIncome(realEstateId, incomeId);
        if (!isDeleted)
            throw new EntityNotFoundException(realEstateId.ToString());
    }

    public async Task<IReadOnlyCollection<RealEstate>> GetAllRealEstates()
    {
        return await realEstateRepository.GetAllRealEstates();
    }

    public async Task DeleteRealEstate(int id)
    {
        var realEstateDeleted = await realEstateRepository.DeleteRealEstate(id);

        if (realEstateDeleted == false)
            throw new EntityNotFoundException(id.ToString());
    }

    public async Task<RealEstate> GetRealEstate(int id)
    {
        var realEstate = await realEstateRepository.GetRealEstate(id);
        if (realEstate == null)
            throw new EntityNotFoundException(id.ToString());
        return realEstate;
    }

    public async Task<RealEstate> UpdateRealEstate(int id, RealEstatePost realEstate)
    {
        var realEstateResult = await realEstateRepository.UpdateRealEstate(id, realEstate);

        if (realEstateResult == null)
        {
            throw new EntityNotFoundException(id.ToString());
        }

        return realEstateResult;
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