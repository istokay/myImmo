using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using MyImmo.App.Dtos;

namespace MyImmo.Api.Dtos;

public class RealEstateResponseDto
{
    [JsonPropertyName("id")]
    [NotNull]
    public required int Id { get; set; }
    [JsonPropertyName("income")]
    [AllowNull]
    public List<RealEstate>? RealEstates { get; set; }
}