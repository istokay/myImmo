namespace MyImmo.Domain.Dtos;

public class Expenses
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Amount { get; set; }
    public required PaymentRange PaymentCategory { get; set; }
    public int RealEstateId { get; set; }
    public RealEstate? RealEstate { get; set; }
}