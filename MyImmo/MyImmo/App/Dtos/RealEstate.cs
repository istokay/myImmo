namespace MyImmo.App.Dtos;

public class RealEstate
{
    public int Id { get; set; }
    public IReadOnlyCollection<Income>? Incomes { get; set; }
}