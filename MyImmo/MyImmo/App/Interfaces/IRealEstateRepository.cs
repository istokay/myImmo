using MyImmo.App.Dtos;

namespace MyImmo.App.Interfaces;

public interface IRealEstateRepository
{
    Task<RealEstate> GetRealEstate(int id);
    Task<IReadOnlyCollection<RealEstate>> GetAllRealEstates();
    Task<RealEstate> CreateRealEstate(RealEstatePost realEstate);
    Task<RealEstate> UpdateRealEstate(int id, RealEstatePost realEstate);
    Task<bool> DeleteRealEstateIncome(int realEstateId, int incomeId);
}