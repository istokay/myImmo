using MyImmo.App.Dtos;
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

    public async Task<RealEstate> GetRealEstate(int id)
    {
        return await realEstateRepository.GetRealEstate(id);
    }

    public async Task<RealEstate> UpdateRealEstate(int id, RealEstatePost realEstate)
    {
        return await realEstateRepository.UpdateRealEstate(id, realEstate);
    }
}