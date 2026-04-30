using MyImmo.Domain.Dtos;

namespace MyImmo.App.Interfaces;

public interface IIncomeRepository
{
    Task<IReadOnlyCollection<Income>?> GetImcomes(int realEstateId);
    Task<Income?> CreateIncome(int realEstateId, IncomePost incomePost);
    Task<Income?> UpdateIncome(int realEstateId, int incomeId, IncomePost incomePost);
    Task<bool> DeleteRealEstateIncome(int realEstateId, int incomeId);
}