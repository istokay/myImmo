using System.Diagnostics.CodeAnalysis;

namespace MyImmo.Api.Dtos;

public class GetRealEstateRequestDto
{
    [NotNull]
    public required int Id { get; set; }
}