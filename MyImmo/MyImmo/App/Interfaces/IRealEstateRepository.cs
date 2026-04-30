using MyImmo.Domain.Dtos;

namespace MyImmo.App.Interfaces;

public interface IRealEstateRepository
{
    Task<RealEstate?> GetRealEstate(int id);
    Task<IReadOnlyCollection<Income>?> GetImcomes(int realEstateId);
    Task<IReadOnlyCollection<RealEstate>> GetAllRealEstates();
    Task<RealEstate> CreateRealEstate(RealEstatePost realEstate);
    Task<Income?> CreateIncome(int realEstateId, IncomePost incomePost);
    Task<RealEstate?> UpdateRealEstate(int id, RealEstatePost realEstate);
    Task<Income?> UpdateIncome(int realEstateId, int incomeId, IncomePost incomePost);
    Task<bool> DeleteRealEstateIncome(int realEstateId, int incomeId);
    Task<bool> DeleteRealEstate(int id);
}