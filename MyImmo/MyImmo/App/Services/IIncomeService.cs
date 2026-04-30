using MyImmo.Domain.Dtos;

namespace MyImmo.App.Services;

public interface IIncomeService
{
    Task<IReadOnlyCollection<Income>> GetImcomes(int realEstateId);
    Task<Income> CreateIncome(int realEstateId, IncomePost incomePost);
    Task<Income> UpdateIncome(int realEstateId, int incomeId, IncomePost incomePost);
    Task DeleteRealEstateIncome(int realEstateId, int incomeId);
}