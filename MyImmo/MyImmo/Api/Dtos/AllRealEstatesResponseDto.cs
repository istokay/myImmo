using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using MyImmo.Domain.Dtos;

namespace MyImmo.Api.Dtos;

public class AllRealEstatesResponseDto
{
    [JsonPropertyName("id")]
    [NotNull]
    public required int Id { get; set; }
    [JsonPropertyName("realEstate")]
    [AllowNull]
    public RealEstatePost? RealEstates { get; set; }
}