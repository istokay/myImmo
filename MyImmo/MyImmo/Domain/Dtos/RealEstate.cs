using MyImmo.Domain.Entities;

namespace MyImmo.Domain.Dtos;

public class RealEstate
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public IReadOnlyCollection<Income> Incomes { get; set; } = new List<Income>();
    public IReadOnlyCollection<Expenses> Expenses { get; set; } = new List<Expenses>();
}