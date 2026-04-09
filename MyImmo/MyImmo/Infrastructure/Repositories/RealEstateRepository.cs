using Microsoft.EntityFrameworkCore;
using MyImmo.App.Dtos;
using MyImmo.App.Interfaces;
using MyImmo.Domain.Entities;
using MyImmo.Domain.Infrastructure.Database;

namespace MyImmo.Infrastructure.Repositories;

public class RealEstateRepository(RealEstateDbContext dbContext) : IRealEstateRepository
{
    public async Task<RealEstate> CreateRealEstate(RealEstatePost realEstate)
    {
        var entity = dbContext.RealEstates.Add(new RealEstateEntity
        {
            Incomes = realEstate.Incomes?.Select(i => new IncomeEntity
            {
                Amount = i.Amount,
                Category = i.IncomeCategory
            }).ToList()
        });

        await dbContext.SaveChangesAsync();

        var result = new RealEstate
        {
            Id = entity.Entity.Id,
            Incomes = entity.Entity.Incomes != null ?
            entity.Entity.Incomes.Select(i =>
                new Income
                {
                    Id = i.Id,
                    Amount = i.Amount,
                    IncomeCategory = i.Category
                }
            ).ToList() : null
        };
        return result;
    }

    public Task DeleteRealEstate(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<RealEstate>> GetAllRealEstates()
    {
        var result = await dbContext.RealEstates.Select(entitie => new RealEstate
        {
            Id = entitie.Id,
            Incomes = entitie.Incomes != null ? entitie.Incomes.Select(i => new Income
            {
                Amount = i.Amount,
                IncomeCategory = i.Category
            }).ToList() : null
        }).ToListAsync();

        return result;
    }

    public Task<RealEstate> GetRealEstate(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RealEstate> UpdateRealEstate(int id, RealEstatePost realEstate)
    {
        throw new NotImplementedException();
    }
}