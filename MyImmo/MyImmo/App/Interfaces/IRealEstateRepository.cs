using MyImmo.App.Dtos;

namespace MyImmo.App.Interfaces;

public interface IRealEstateRepository
{
    Task<RealEstate> GetRealEstate(int id);
    Task<List<RealEstate>> GetAllRealEstates();
    Task<RealEstate> CreateRealEstate(RealEstate realEstate);
    Task<RealEstate> UpdateRealEstate(int id, RealEstate realEstate);
    Task DeleteRealEstate(int id);
}