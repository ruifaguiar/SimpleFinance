using System.Net;
using System.Net.Http.Json;
using SimpleFinance.WebApi.Model;
using SimpleFinance.WebApi.Tests.Infrastructure;

namespace SimpleFinance.WebApi.Tests.Endpoints.AccountTypes.Integration;

public class AccountTypeCrudTests : IClassFixture<SimpleFinanceWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly SimpleFinanceWebApplicationFactory _factory;

    public AccountTypeCrudTests(SimpleFinanceWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task FullCrudCycle_ShouldWorkCorrectly()
    {
        // Create
        var createModel = new AccountTypeModel { Name = "CRUD Test", Description = "Full CRUD test" };
        var createResponse = await _client.PostAsJsonAsync("/api/account-type", createModel);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var created = await createResponse.Content.ReadFromJsonAsync<AccountTypeModel>();
        Assert.NotNull(created);

        // Read
        var getResponse = await _client.GetAsync($"/api/account-type/{created.Id}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        var retrieved = await getResponse.Content.ReadFromJsonAsync<AccountTypeModel>();
        Assert.Equal(created.Name, retrieved!.Name);

        // Update
        var updateModel = new AccountTypeModel { Name = "CRUD Test Updated", Description = "Updated" };
        var updateResponse = await _client.PutAsJsonAsync($"/api/account-type/{created.Id}", updateModel);
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        var updated = await updateResponse.Content.ReadFromJsonAsync<AccountTypeModel>();
        Assert.Equal("CRUD Test Updated", updated!.Name);

        // Delete
        var deleteResponse = await _client.DeleteAsync($"/api/account-type/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        // Verify deleted
        var verifyResponse = await _client.GetAsync($"/api/account-type/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, verifyResponse.StatusCode);
    }
}

