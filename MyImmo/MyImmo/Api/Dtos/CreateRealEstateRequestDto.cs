using MyImmo.App.Dtos;

namespace MyImmo.Api.Dtos;

public class CreateRealEstateRequestDto
{
    public List<IncomePost>? Incomes { get; set; }
}