namespace MyImmo.Domain.Entities;

public class RealEstateEntity 
{
    public required int Id { get; set; }
    public ICollection<IncomeEntity>? Incomes { get; set; }
    
}