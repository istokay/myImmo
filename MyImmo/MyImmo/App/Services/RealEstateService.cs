using MyImmo.App.Dtos;
using MyImmo.App.Interfaces;

namespace MyImmo.App.Services;

public class RealEstateService(IRealEstateRepository realEstateRepository) : IRealEstateService
{
    public Task<RealEstate> CreateRealEstate(RealEstate realEstate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRealEstate(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<RealEstate>> GetAllRealEstates()
    {
        throw new NotImplementedException();
    }

    public Task<RealEstate> GetRealEstate(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RealEstate> UpdateRealEstate(int id, RealEstate realEstate)
    {
        throw new NotImplementedException();
    }
}