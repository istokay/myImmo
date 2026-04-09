namespace MyImmo.App.Dtos;

public class IncomePost
{
    public required decimal Amount { get; set; }

    public required IncomeCategory IncomeCategory { get; set; }
}