using MyImmo.Domain.Dtos;

namespace MyImmo.Domain.Entities;

public class ExpensesEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required decimal Amount { get; set; }
    public required PaymentRange PaymentRange { get; set; }
    public int RealEstateId { get; set; }
    public RealEstateEntity? RealEstate { get; set; }
    public required DateTime InitialDate { get; set; }
}