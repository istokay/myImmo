using Microsoft.EntityFrameworkCore;
using MyImmo.Domain.Dtos;
using MyImmo.App.Interfaces;
using MyImmo.Domain.Entities;
using MyImmo.Domain.Infrastructure.Database;

namespace MyImmo.Infrastructure.Repositories;

public class IncomeRepository(RealEstateDbContext dbContext) : IIncomeRepository
{

    public async Task<Income?> CreateIncome(int realEstateId, IncomePost incomePost)
    {
        var realEstate = await dbContext.RealEstates.FirstOrDefaultAsync(re => re.Id == realEstateId);

        if (realEstate == null)
            return null;

        var entity = dbContext.Add(new IncomeEntity
        {
            Name = incomePost.Name,
            Amount = incomePost.Amount,
            Category = incomePost.IncomeCategory,
            RealEstateId = realEstateId,
        });

        await dbContext.SaveChangesAsync();

        return new Income
        {
            Id = entity.Entity.Id,
            Name = entity.Entity.Name,
            Amount = entity.Entity.Amount,
            RealEstateId = entity.Entity.RealEstateId,
            IncomeCategory = entity.Entity.Category
        };
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

    public async Task<IReadOnlyCollection<Income>?> GetImcomes(int realEstateId)
    {
        var realEstate = await dbContext.RealEstates.FirstOrDefaultAsync(re => re.Id == realEstateId);

        if (realEstate == null)
            return null;

        var entities = await dbContext.Incomes.Where(i => i.RealEstateId == realEstateId).Select(i => new Income
        {
            Id = i.Id,
            Name = i.Name,
            Amount = i.Amount,
            IncomeCategory = i.Category,
            RealEstateId = i.RealEstateId
        }).ToListAsync();

        return entities;
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
}