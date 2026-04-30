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