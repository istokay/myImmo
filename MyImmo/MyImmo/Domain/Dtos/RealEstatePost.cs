namespace MyImmo.Domain.Dtos;

public class RealEstatePost
{
    public required string Name { get; set; }
    public IReadOnlyCollection<IncomePost>? Incomes { get; set; }
    public IReadOnlyCollection<ExpensesPost>? Expenses { get; set; }
}