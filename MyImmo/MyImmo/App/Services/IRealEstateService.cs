using MyImmo.App.Dtos;

namespace MyImmo.App.Services;

public interface IRealEstateService
{
    Task<RealEstate> GetRealEstate(int id);
    Task<List<RealEstate>> GetAllRealEstates();
    Task<RealEstate> CreateRealEstate(RealEstate realEstate);
    Task<RealEstate> UpdateRealEstate(int id, RealEstate realEstate);
    Task DeleteRealEstate(int id);
}