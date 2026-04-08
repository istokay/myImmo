using MyImmo.App.Dtos;
using MyImmo.App.Interfaces;

namespace MyImmo.App.Services;

public class RealEstateService(IRealEstateRepository realEstateRepository) : IRealEstateService
{
    public async Task<RealEstate> CreateRealEstate(RealEstate realEstate)
    {
       return await realEstateRepository.CreateRealEstate(realEstate);
    }

    public async Task DeleteRealEstate(int id)
    {
        await realEstateRepository.DeleteRealEstate(id);
    }

    public async Task<List<RealEstate>> GetAllRealEstates()
    {
        return await realEstateRepository.GetAllRealEstates();
    }

    public async Task<RealEstate> GetRealEstate(int id)
    {
        return await realEstateRepository.GetRealEstate(id);
    }

    public async Task<RealEstate> UpdateRealEstate(int id, RealEstate realEstate)
    {
        return await realEstateRepository.UpdateRealEstate(id, realEstate);
    }
}