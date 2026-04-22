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
            Name = realEstate.Name,
            Incomes = realEstate.Incomes?.Select(i => new IncomeEntity
            {
                Name = i.Name,
                Amount = i.Amount,
                Category = i.IncomeCategory
            }).ToList()
        });

        await dbContext.SaveChangesAsync();

        var result = new RealEstate
        {
            Id = entity.Entity.Id,
            Name = entity.Entity.Name,
            Incomes = entity.Entity.Incomes != null ?
            entity.Entity.Incomes.Select(i =>
                new Income
                {
                    Id = i.Id,
                    Name = i.Name,
                    Amount = i.Amount,
                    IncomeCategory = i.Category
                }
            ).ToList() : null
        };
        return result;
    }

    public async Task<bool> DeleteRealEstateIncome(int realEstateId, int incomeId)
    {
        var isDeleted = true;

        var income = await dbContext.Incomes.FirstOrDefaultAsync(i => i.Id == realEstateId && i.RealEstateId == realEstateId);

        if (income == null)
        {
            return !isDeleted;
        }
        dbContext.Incomes.Remove(income);

        await dbContext.SaveChangesAsync();

        return isDeleted;
    }

    public async Task<IReadOnlyCollection<RealEstate>> GetAllRealEstates()
    {
        var result = await dbContext.RealEstates.Select(entitie => new RealEstate
        {
            Name = entitie.Name,
            Id = entitie.Id,
            Incomes = entitie.Incomes != null ? entitie.Incomes.Select(i => new Income
            {
                Name = i.Name,
                Amount = i.Amount,
                IncomeCategory = i.Category
            }).ToList() : null
        }).ToListAsync();

        return result;
    }

    public async Task<RealEstate?> GetRealEstate(int id)
    {
        var realEstate = await dbContext.RealEstates
        .Include(re => re.Incomes)
        .FirstOrDefaultAsync(re => re.Id == id);

        if (realEstate == null)
            return null;

        return new RealEstate
        {
            Name = realEstate.Name,
            Id = realEstate.Id,
            Incomes = realEstate.Incomes?.Select(i => new Income
            {
                Id = i.Id,
                Name = i.Name,
                Amount = i.Amount,
                IncomeCategory = i.Category
            }).ToList()
        };
    }

    public async Task<RealEstate?> UpdateRealEstate(int id, RealEstatePost realEstate)
    {
        var entity = await dbContext.RealEstates.LastOrDefaultAsync(re => re.Id == id);

        if (entity == null)
        {
            return null;
        }

        return new RealEstate
        {
            Id = entity.Id,
            Name = entity.Name,
            Incomes = entity.Incomes?.Select(i => new Income
            {
                Name = i.Name,
                Amount = i.Amount,
                IncomeCategory = i.Category
            }).ToArray()
        };
    }

    public async Task<bool> DeleteRealEstate(int id)
    {
        var isDeleted = true;

        var realEstate = await dbContext.RealEstates.FirstOrDefaultAsync(re => re.Id == id);

        if (realEstate == null)
            return !isDeleted;

        dbContext.RealEstates.Remove(realEstate);

        await dbContext.SaveChangesAsync();

        return isDeleted;
    }
}