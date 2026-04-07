using System.Diagnostics.CodeAnalysis;

namespace MyImmo.Api.Dtos;

public class RealEstateRequestDto
{
    [NotNull]
    public required int Id { get; set; }
}