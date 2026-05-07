using MyImmo.Domain.Dtos;

namespace MyImmo.App.Services;

public interface IRealEstateService
{
    IReadOnlyCollection<RealEstate> GetAllRealEstates();
    RealEstate CreateRealEstate(RealEstatePost realEstate);
    RealEstate UpdateRealEstate(int id, RealEstatePost realEstate);
    void DeleteRealEstate(int id);

}