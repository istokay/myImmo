using MyImmo.Domain.Dtos;

namespace MyImmo.App.Interfaces;

public interface IRealEstateRepository
{
    IReadOnlyCollection<RealEstate> GetAllRealEstates();
    RealEstate CreateRealEstate(RealEstatePost realEstate);
    RealEstate? UpdateRealEstate(int id, RealEstatePost realEstate);
    bool DeleteRealEstate(int id);
}