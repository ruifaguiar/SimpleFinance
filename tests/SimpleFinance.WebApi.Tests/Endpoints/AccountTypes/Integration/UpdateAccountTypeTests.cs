using System.Net;
using System.Net.Http.Json;
using SimpleFinance.WebApi.Model;
using SimpleFinance.WebApi.Tests.Infrastructure;

namespace SimpleFinance.WebApi.Tests.Endpoints.AccountTypes.Integration;

public class UpdateAccountTypeTests : IClassFixture<SimpleFinanceWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly SimpleFinanceWebApplicationFactory _factory;

    public UpdateAccountTypeTests(SimpleFinanceWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task UpdateAccountType_WithValidModel_ShouldReturnOk()
    {
        // Create first
        var createResponse = await _client.PostAsJsonAsync("/api/account-type", 
            new AccountTypeModel { Name = "To Update" });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var created = await createResponse.Content.ReadFromJsonAsync<AccountTypeModel>();
        Assert.NotNull(created);
        Assert.NotNull(created.Id);

        // Update
        var updateModel = new AccountTypeModel { Id = created.Id, Name = "Updated Name" };
        var response = await _client.PutAsJsonAsync($"/api/account-type/{created.Id}", updateModel);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAccountType_WithValidModel_ShouldReturnUpdatedAccountType()
    {
        // Create first
        var createResponse = await _client.PostAsJsonAsync("/api/account-type", 
            new AccountTypeModel { Name = "Original Name", Description = "Original" });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var created = await createResponse.Content.ReadFromJsonAsync<AccountTypeModel>();
        Assert.NotNull(created);
        Assert.NotNull(created.Id);

        // Update
        var updateModel = new AccountTypeModel 
        { 
            Id = created.Id, 
            Name = "Updated Name", 
            Description = "Updated Description" 
        };
        var response = await _client.PutAsJsonAsync($"/api/account-type/{created.Id}", updateModel);
        var updated = await response.Content.ReadFromJsonAsync<AccountTypeModel>();

        Assert.NotNull(updated);
        Assert.Equal(created.Id, updated.Id);
        Assert.Equal("Updated Name", updated.Name);
        Assert.Equal("Updated Description", updated.Description);
    }

    [Fact]
    public async Task UpdateAccountType_WithNonExistingId_ShouldReturnNotFound()
    {
        var model = new AccountTypeModel { Name = "Test" };

        var response = await _client.PutAsJsonAsync("/api/account-type/99999", model);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAccountType_WithMismatchedIds_ShouldReturnBadRequest()
    {
        // Create first
        var createResponse = await _client.PostAsJsonAsync("/api/account-type", 
            new AccountTypeModel { Name = "Mismatch Test" });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var created = await createResponse.Content.ReadFromJsonAsync<AccountTypeModel>();
        Assert.NotNull(created);
        Assert.NotNull(created.Id);

        // Try to update with mismatched ID
        var updateModel = new AccountTypeModel { Id = 99999, Name = "Test" };
        var response = await _client.PutAsJsonAsync($"/api/account-type/{created.Id}", updateModel);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateAccountType_WithNullModelId_ShouldSucceed()
    {
        // Create first
        var createResponse = await _client.PostAsJsonAsync("/api/account-type", 
            new AccountTypeModel { Name = "Null Id Update Test" });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var created = await createResponse.Content.ReadFromJsonAsync<AccountTypeModel>();
        Assert.NotNull(created);
        Assert.NotNull(created.Id);

        // Update with null ID in body
        var updateModel = new AccountTypeModel { Id = null, Name = "Updated With Null Id" };
        var response = await _client.PutAsJsonAsync($"/api/account-type/{created.Id}", updateModel);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var updated = await response.Content.ReadFromJsonAsync<AccountTypeModel>();
        Assert.Equal("Updated With Null Id", updated!.Name);
    }
}

