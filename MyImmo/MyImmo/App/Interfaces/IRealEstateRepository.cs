using MyImmo.Domain.Dtos;

namespace MyImmo.App.Interfaces;

public interface IRealEstateRepository
{
    Task<IReadOnlyCollection<RealEstate>> GetAllRealEstates();
    Task<RealEstate> CreateRealEstate(RealEstatePost realEstate);
    Task<RealEstate?> UpdateRealEstate(int id, RealEstatePost realEstate);
    Task<bool> DeleteRealEstate(int id);
}