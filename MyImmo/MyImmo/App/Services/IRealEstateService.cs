using MyImmo.App.Dtos;

namespace MyImmo.App.Services;

public interface IRealEstateService
{
    Task<Income> GetRealEstate(int id);
    Task<Incomes> GetAllRealEstates();
    Task<Income> CreateRealEstate(Income income);
    Task<Income> UpdateRealEstate(int id, Income income);
    Task DeleteRealEstate(int id);
}