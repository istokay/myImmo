using MyImmo.Domain.Dtos;

namespace MyImmo.App.Services;

public interface IRealEstateService
{
    Task<IReadOnlyCollection<RealEstate>> GetAllRealEstates();
    Task<RealEstate> CreateRealEstate(RealEstatePost realEstate);
    Task<RealEstate> UpdateRealEstate(int id, RealEstatePost realEstate);
    Task DeleteRealEstate(int id);

}