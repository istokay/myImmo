using MyImmo.App.Dtos;

namespace MyImmo.Domain.Entities;

public class IncomeEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Amount { get; set; }
    public required IncomeCategory Category { get; set; }
    public int RealEstateId { get; set; }
    public RealEstateEntity? RealEstate { get; set; }
}