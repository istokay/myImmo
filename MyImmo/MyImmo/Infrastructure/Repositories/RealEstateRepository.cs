using Microsoft.EntityFrameworkCore;
using MyImmo.Domain.Dtos;
using MyImmo.App.Interfaces;
using MyImmo.Domain.Entities;
using MyImmo.Domain.Infrastructure.Database;

namespace MyImmo.Infrastructure.Repositories;

public class RealEstateRepository(RealEstateDbContext dbContext) : IRealEstateRepository
{
    public RealEstate CreateRealEstate(RealEstatePost realEstate)
    {
        var entity = dbContext.RealEstates.Add(MapToEntity(realEstate));

        dbContext.SaveChanges();

        return MapToDomain(entity.Entity);
    }

    public IReadOnlyCollection<RealEstate> GetAllRealEstates()
    {
        var result = dbContext.RealEstates.Select(entitie => new RealEstate
        {
            Name = entitie.Name,
            Id = entitie.Id
        }).ToArray();

        return result;
    }

    public RealEstate? UpdateRealEstate(int id, RealEstatePost realEstate)
    {
        var entity = dbContext.RealEstates.Include(re => re.Incomes).SingleOrDefault(re => re.Id == id);

        if (entity == null)
        {
            return null;
        }

        entity.Name = realEstate.Name;

        dbContext.Incomes.RemoveRange(entity.Incomes);

        entity.Incomes = realEstate.Incomes?.Select(MapIncomeToEntity).ToList() ?? new List<IncomeEntity>();

        dbContext.SaveChanges();

        return MapToDomain(entity);
    }

    public bool DeleteRealEstate(int id)
    {
        var isDeleted = true;

        var realEstate = dbContext.RealEstates.Include(re => re.Incomes).SingleOrDefault(re => re.Id == id);

        if (realEstate == null)
            return !isDeleted;

        dbContext.RealEstates.Remove(realEstate);

        dbContext.SaveChanges();

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