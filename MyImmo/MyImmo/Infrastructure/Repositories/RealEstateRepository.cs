using MyImmo.App.Dtos;
using MyImmo.App.Interfaces;
using MyImmo.Domain.Entities;
using MyImmo.Domain.Infrastructure.Database;

namespace MyImmo.Infrastructure.Repositories;

public class RealEstateRepository(RealEstateDbContext dbContext) : IRealEstateRepository
{
    public Task<RealEstate> CreateRealEstate(RealEstate realEstate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRealEstate(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<RealEstate>> GetAllRealEstates()
    {
        throw new NotImplementedException();
    }

    public Task<RealEstate> GetRealEstate(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RealEstate> UpdateRealEstate(int id, RealEstate realEstate)
    {
        throw new NotImplementedException();
    }
}