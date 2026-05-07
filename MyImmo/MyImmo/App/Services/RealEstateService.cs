using MyImmo.Domain.Dtos;
using MyImmo.App.Exceptions;
using MyImmo.App.Interfaces;

namespace MyImmo.App.Services;

public class RealEstateService(IRealEstateRepository realEstateRepository) : IRealEstateService
{
    public RealEstate CreateRealEstate(RealEstatePost realEstate)
    {
        return realEstateRepository.CreateRealEstate(realEstate);
    }

    public IReadOnlyCollection<RealEstate> GetAllRealEstates()
    {
        return realEstateRepository.GetAllRealEstates();
    }

    public void DeleteRealEstate(int id)
    {
        var realEstateDeleted = realEstateRepository.DeleteRealEstate(id);

        if (realEstateDeleted == false)
            throw new EntityNotFoundException(id.ToString());
    }

    public RealEstate UpdateRealEstate(int id, RealEstatePost realEstate)
    {
        var realEstateResult = realEstateRepository.UpdateRealEstate(id, realEstate);

        if (realEstateResult == null)
        {
            throw new EntityNotFoundException(id.ToString());
        }

        return realEstateResult;
    }
}