namespace MyImmo.Domain.Dtos;

public class Income
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Amount { get; set; }
    public required IncomeCategory IncomeCategory { get; set; }
    public int RealEstateId { get; set; }
    public required RealEstate RealEstate { get; set; }
}