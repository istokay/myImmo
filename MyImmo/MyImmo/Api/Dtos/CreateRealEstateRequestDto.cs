using MyImmo.Domain.Dtos;

namespace MyImmo.Api.Dtos;

public class CreateRealEstateRequestDto
{
    public required string Name { get; set; }
    public List<IncomePost>? Incomes { get; set; }
}