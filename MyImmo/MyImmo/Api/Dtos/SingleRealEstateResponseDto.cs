using System.Text.Json.Serialization;
using MyImmo.App.Dtos;

namespace MyImmo.Api.Dtos;

public class SingleRealEstateResponseDto
{
    [JsonPropertyName("id")]
    public int Id {get; set;}

    [JsonPropertyName("realEstate")]
    public required RealEstate RealEstate {get; set;}
}