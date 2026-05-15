using System.Net;
using System.Net.Http.Json;
using MyImmo.Domain.Dtos;
using Xunit;

namespace MyImmo.IntegrationTests.HTTP;

public class RealEstateTests
{
    [Fact]
    public async Task GET_All_WhenNoRealEstates_ShouldReturnNotFound()
    {
        await using var factory = new TestWebApplicationFactory();
        var client = factory.CreateClient();

        var response = await client.GetAsync("/api/RealEstate");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task POST_RealEstate_ShouldSave_And_GetAllReturnsEntity()
    {
        await using var factory = new TestWebApplicationFactory();
        var client = factory.CreateClient();

        var request = new RealEstatePost
        {
            Name = "Real Estate1"
        };

        var createResponse = await client.PostAsJsonAsync("/api/RealEstate", request);
        createResponse.EnsureSuccessStatusCode();

        var created = await createResponse.Content.ReadFromJsonAsync<RealEstate>();
        Assert.NotNull(created);
        Assert.Equal("Real Estate1", created!.Name);
        Assert.True(created.Id > 0);

        var getResponse = await client.GetAsync("/api/RealEstate");
        getResponse.EnsureSuccessStatusCode();

        var collection = await getResponse.Content.ReadFromJsonAsync<List<RealEstate>>();
        Assert.NotNull(collection);
        Assert.Contains(collection, item => item.Id == created.Id && item.Name == "Real Estate1");
    }

    [Fact]
    public async Task PUT_RealEstate_ShouldUpdate_ExistingEntity()
    {
        await using var factory = new TestWebApplicationFactory();
        var client = factory.CreateClient();

        var createRequest = new RealEstatePost { Name = "Original Estate" };
        var createResponse = await client.PostAsJsonAsync("/api/RealEstate", createRequest);
        createResponse.EnsureSuccessStatusCode();

        var created = await createResponse.Content.ReadFromJsonAsync<RealEstate>();
        Assert.NotNull(created);

        var updateRequest = new RealEstatePost { Name = "Updated Estate" };
        var updateResponse = await client.PutAsJsonAsync($"/api/RealEstate/{created!.Id}", updateRequest);
        updateResponse.EnsureSuccessStatusCode();

        var updated = await updateResponse.Content.ReadFromJsonAsync<RealEstate>();
        Assert.NotNull(updated);
        Assert.Equal(created.Id, updated!.Id);
        Assert.Equal("Updated Estate", updated.Name);

        var getResponse = await client.GetAsync("/api/RealEstate");
        getResponse.EnsureSuccessStatusCode();

        var collection = await getResponse.Content.ReadFromJsonAsync<List<RealEstate>>();
        Assert.NotNull(collection);
        Assert.Contains(collection, item => item.Id == created.Id && item.Name == "Updated Estate");
    }

    [Fact]
    public async Task DELETE_RealEstate_ShouldRemove_Entity()
    {
        await using var factory = new TestWebApplicationFactory();
        var client = factory.CreateClient();

        var createRequest = new RealEstatePost { Name = "Estate To Delete" };
        var createResponse = await client.PostAsJsonAsync("/api/RealEstate", createRequest);
        createResponse.EnsureSuccessStatusCode();

        var created = await createResponse.Content.ReadFromJsonAsync<RealEstate>();
        Assert.NotNull(created);

        var deleteResponse = await client.DeleteAsync($"/api/RealEstate/{created!.Id}");
        deleteResponse.EnsureSuccessStatusCode();

        var getResponse = await client.GetAsync("/api/RealEstate");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DELETE_Nonexistent_RealEstate_ShouldReturnNotFound()
    {
        await using var factory = new TestWebApplicationFactory();
        var client = factory.CreateClient();

        var response = await client.DeleteAsync("/api/RealEstate/9999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task PUT_Nonexistent_RealEstate_ShouldReturnNotFound()
    {
        await using var factory = new TestWebApplicationFactory();
        var client = factory.CreateClient();

        var updateRequest = new RealEstatePost { Name = "Does Not Exist" };
        var response = await client.PutAsJsonAsync("/api/RealEstate/9999", updateRequest);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
