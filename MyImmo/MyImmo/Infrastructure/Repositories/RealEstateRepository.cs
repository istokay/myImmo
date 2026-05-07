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
        var entity = dbContext.RealEstates.Add(MapToEntity(realEstate));

        await dbContext.SaveChangesAsync();

        return MapToDomain(entity.Entity);
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

    public async Task<RealEstate?> UpdateRealEstate(int id, RealEstatePost realEstate)
    {
        var entity = await dbContext.RealEstates.Include(re => re.Incomes).SingleOrDefaultAsync(re => re.Id == id);

        if (entity == null)
        {
            return null;
        }

        entity.Name = realEstate.Name;

        dbContext.Incomes.RemoveRange(entity.Incomes);

        entity.Incomes = realEstate.Incomes?.Select(MapIncomeToEntity).ToList() ?? new List<IncomeEntity>();

        await dbContext.SaveChangesAsync();

        return MapToDomain(entity);
    }

    public async Task<bool> DeleteRealEstate(int id)
    {
        var isDeleted = true;

        var realEstate = await dbContext.RealEstates.Include(re => re.Incomes).SingleOrDefaultAsync(re => re.Id == id);

        if (realEstate == null)
            return !isDeleted;

        dbContext.RealEstates.Remove(realEstate);

        await dbContext.SaveChangesAsync();

        return isDeleted;
    }

    private static RealEstateEntity MapToEntity(RealEstatePost realEstate)
    {
        return new RealEstateEntity
        {
            Name = realEstate.Name,
            Incomes = realEstate.Incomes?.Select(i => new IncomeEntity
            {
                Name = i.Name,
                Amount = i.Amount,
                Category = i.IncomeCategory
            }).ToList() ?? new List<IncomeEntity>()
        };
    }

    private static RealEstate MapToDomain(RealEstateEntity entity)
    {
        var result = new RealEstate
        {
            Id = entity.Id,
            Name = entity.Name,
            Incomes = entity.Incomes.Select(i => new Income
            {
                Name = i.Name,
                Amount = i.Amount,
                IncomeCategory = i.Category
            }).ToList()
        };
        return result;
    }

    private static IncomeEntity MapIncomeToEntity(IncomePost income)
    {
        return new IncomeEntity
        {
            Name = income.Name,
            Amount = income.Amount,
            Category = income.IncomeCategory
        };
    }
}