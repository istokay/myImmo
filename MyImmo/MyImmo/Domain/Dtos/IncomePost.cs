namespace MyImmo.Domain.Dtos;

public class IncomePost
{
    public required string Name { get; set; }
    public required decimal Amount { get; set; }

    public required IncomeCategory IncomeCategory { get; set; }
}