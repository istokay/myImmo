using MyImmo.Domain.Dtos;

namespace MyImmo.App.Services;

public interface IRealEstateService
{
    Task<RealEstate> GetRealEstate(int id);
    Task<IReadOnlyCollection<RealEstate>> GetAllRealEstates();
    Task<RealEstate> CreateRealEstate(RealEstatePost realEstate);
    Task<RealEstate> UpdateRealEstate(int id, RealEstatePost realEstate);
    Task<Income> UpdateIncome(int realEstateId, int incomeId, IncomePost incomePost);
    Task DeleteRealEstate(int id);
    Task DeleteRealEstateIncome(int realEstateId, int incomeId);
}