namespace MyImmo.Domain.Entities;

public class RealEstateEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<IncomeEntity>? Incomes { get; set; }

}