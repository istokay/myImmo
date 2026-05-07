namespace MyImmo.Domain.Dtos;

public class ExpensesPost
{
    public required string Name { get; set; }
    public required decimal Amount { get; set; }
    public required PaymentRange PaymentRange { get; set; }
    public required DateTime InitialDate { get; set; }
}