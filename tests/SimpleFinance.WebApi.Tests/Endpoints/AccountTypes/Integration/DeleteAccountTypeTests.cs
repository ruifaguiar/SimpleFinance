using System.Net;
using System.Net.Http.Json;
using SimpleFinance.WebApi.Model;
using SimpleFinance.WebApi.Tests.Infrastructure;

namespace SimpleFinance.WebApi.Tests.Endpoints.AccountTypes.Integration;

public class DeleteAccountTypeTests : IClassFixture<SimpleFinanceWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly SimpleFinanceWebApplicationFactory _factory;

    public DeleteAccountTypeTests(SimpleFinanceWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task DeleteAccountType_WithExistingId_ShouldReturnNoContent()
    {
        // Create first
        var createResponse = await _client.PostAsJsonAsync("/api/account-type", 
            new AccountTypeModel { Name = "To Delete" });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var created = await createResponse.Content.ReadFromJsonAsync<AccountTypeModel>();
        Assert.NotNull(created);
        Assert.NotNull(created.Id);

        var response = await _client.DeleteAsync($"/api/account-type/{created.Id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeleteAccountType_WithExistingId_ShouldRemoveAccountType()
    {
        // Create first
        var createResponse = await _client.PostAsJsonAsync("/api/account-type", 
            new AccountTypeModel { Name = "To Delete And Verify" });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var created = await createResponse.Content.ReadFromJsonAsync<AccountTypeModel>();
        Assert.NotNull(created);
        Assert.NotNull(created.Id);

        // Delete
        await _client.DeleteAsync($"/api/account-type/{created.Id}");

        // Verify deleted
        var getResponse = await _client.GetAsync($"/api/account-type/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Fact]
    public async Task DeleteAccountType_WithNonExistingId_ShouldReturnNotFound()
    {
        var response = await _client.DeleteAsync("/api/account-type/99999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}

