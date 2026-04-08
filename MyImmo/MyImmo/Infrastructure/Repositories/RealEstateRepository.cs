using MyImmo.App.Dtos;
using MyImmo.App.Interfaces;
using MyImmo.Domain.Entities;
using MyImmo.Domain.Infrastructure.Database;

namespace MyImmo.Infrastructure.Repositories;

public class RealEstateRepository(RealEstateDbContext dbContext) : IRealEstateRepository
{
    public async Task<Income> CreateRealEstate(Income income)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRealEstate(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Incomes> GetAllRealEstates()
    {
        throw new NotImplementedException();
    }

    public Task<Income> GetRealEstate(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Income> UpdateRealEstate(int id, Income income)
    {
        throw new NotImplementedException();
    }
}