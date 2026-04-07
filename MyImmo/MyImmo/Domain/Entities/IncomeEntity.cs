using MyImmo.App.Dtos;

namespace MyImmo.Domain.Entities;

public class IncomeEntity
{
    public required int Id { get; set; }
    public required decimal Amount { get; set; }
    public required IncomeCategory Category { get; set; }
}