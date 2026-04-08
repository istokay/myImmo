using Microsoft.EntityFrameworkCore;
using MyImmo.App.Dtos;
using MyImmo.App.Interfaces;
using MyImmo.Domain.Entities;
using MyImmo.Domain.Infrastructure.Database;

namespace MyImmo.Infrastructure.Repositories;

public class RealEstateRepository(RealEstateDbContext dbContext) : IRealEstateRepository
{
    public async Task<RealEstate> CreateRealEstate(RealEstate realEstate)
    {
        dbContext.RealEstates.Add(new RealEstateEntity
        {
            Id = realEstate.Id,
            Incomes = realEstate.Incomes?.Select(i => new IncomeEntity
            {
                Amount = i.Amount,
                Category = i.Category
            }).ToList()
        });

        await dbContext.SaveChangesAsync();
        return realEstate;
    }

    public Task DeleteRealEstate(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<RealEstate>> GetAllRealEstates()
    {
        var result = await dbContext.RealEstates.Select(entitie => new RealEstate
        {
            Id = entitie.Id,
            Incomes = entitie.Incomes != null ? entitie.Incomes.Select(i => new Income
            {
                Amount = i.Amount,
                Category = i.Category
            }).ToList() : null
        }).ToListAsync(); 

        return result;
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