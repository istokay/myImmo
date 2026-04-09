using System.Text.Json.Serialization;
using MyImmo.App.Dtos;

namespace MyImmo.Api.Dtos;

public class RealEstateResponse
{
    [JsonPropertyName("id")]
    public int Id {get; set;}

    [JsonPropertyName("realEstate")]
    public required RealEstatePost RealEstate {get; set;}
}