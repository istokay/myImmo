namespace MyImmo.Domain.Dtos;

public class RealEstatePost
{
    public required string Name { get; set; }
    public List<IncomePost>? Incomes { get; set; }
}