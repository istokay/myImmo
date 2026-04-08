using MyImmo.App.Dtos;
using MyImmo.App.Interfaces;

namespace MyImmo.App.Services;

public class RealEstateService(IRealEstateRepository realEstateRepository) : IRealEstateService
{
    public Task<Income> CreateRealEstate(Income income)
    {
        return realEstateRepository.CreateRealEstate(income);
    }

    public Task DeleteRealEstate(int id)
    {
        return realEstateRepository.DeleteRealEstate(id);
    }

    public Task<Incomes> GetAllRealEstates()
    {
        return realEstateRepository.GetAllRealEstates();
    }

    public Task<Income> GetRealEstate(int id)
    {
        return realEstateRepository.GetRealEstate(id);
    }

    public Task<Income> UpdateRealEstate(int id, Income income)
    {
        return realEstateRepository.UpdateRealEstate(id, income);
    }
}