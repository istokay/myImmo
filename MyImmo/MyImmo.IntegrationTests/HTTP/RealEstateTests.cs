using System.Net.Http.Json;
using MyImmo.Domain.Dtos;
using Xunit;

namespace MyImmo.IntegrationTests.HTTP;

public class RealEstateTest : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public RealEstateTest(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]

    public async Task POST_Estate_Should_Save_Entity()

    {
        var request = new RealEstatePost
        {
            Name = "Real Estate1",
        };

        var response = await _client.PostAsJsonAsync("/api/RealEstate", request);

        response.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync("/api/RealEstate");

        var content = await getResponse.Content.ReadAsStringAsync();

        Assert.Contains("Real Estate1", content);

    }
}