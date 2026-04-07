namespace MyImmo.App.Dtos;

public class Income
{
    public required decimal Amount { get; set; }

    public required IncomeCategory Category { get; set; }
}