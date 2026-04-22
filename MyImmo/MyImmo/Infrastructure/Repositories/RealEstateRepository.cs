using Microsoft.EntityFrameworkCore;
using MyImmo.Domain.Dtos;
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
            Name = realEstate.Name
        });

        await dbContext.SaveChangesAsync();

        var result = new RealEstate
        {
            Id = entity.Entity.Id,
            Name = entity.Entity.Name,
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
            Id = entitie.Id
        }).ToArrayAsync();

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
        };
    }


    public async Task<Income?> UpdateIncome(int realEstateId, int incomeId, IncomePost incomePost)
    {
        var entity = await dbContext.Incomes.FirstOrDefaultAsync(i => i.Id == incomeId && i.RealEstateId == realEstateId);

        if (entity == null || entity.RealEstate?.Name == null || entity.RealEstate?.Id == null)
        {
            return null;
        }

        return new Income
        {
            Id = entity.Id,
            Name = entity.Name,
            Amount = entity.Amount,
            IncomeCategory = entity.Category,
            RealEstate = new RealEstate
            {
                Name = entity.RealEstate.Name,
                Id = entity.RealEstate.Id
            },
            RealEstateId = entity.RealEstateId
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