using System.Net;
using System.Net.Http.Json;
using SimpleFinance.WebApi.Model;
using SimpleFinance.WebApi.Tests.Infrastructure;

namespace SimpleFinance.WebApi.Tests.Endpoints.AccountTypes.Integration;

public class GetAccountTypeByIdTests : IClassFixture<SimpleFinanceWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly SimpleFinanceWebApplicationFactory _factory;

    public GetAccountTypeByIdTests(SimpleFinanceWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAccountTypeById_WithExistingId_ShouldReturnOk()
    {
        // Create an account type first
        var createResponse = await _client.PostAsJsonAsync("/api/account-type", 
            new AccountTypeModel { Name = "Test GetById", Description = "Test description" });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var created = await createResponse.Content.ReadFromJsonAsync<AccountTypeModel>();
        Assert.NotNull(created);
        Assert.NotNull(created.Id);

        var response = await _client.GetAsync($"/api/account-type/{created.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAccountTypeById_WithExistingId_ShouldReturnCorrectAccountType()
    {
        // Create an account type first
        var createResponse = await _client.PostAsJsonAsync("/api/account-type", 
            new AccountTypeModel { Name = "Unique Name GetById", Description = "Unique description" });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var created = await createResponse.Content.ReadFromJsonAsync<AccountTypeModel>();
        Assert.NotNull(created);
        Assert.NotNull(created.Id);

        var response = await _client.GetAsync($"/api/account-type/{created.Id}");
        var accountType = await response.Content.ReadFromJsonAsync<AccountTypeModel>();

        Assert.NotNull(accountType);
        Assert.Equal(created.Id, accountType.Id);
        Assert.Equal("Unique Name GetById", accountType.Name);
        Assert.Equal("Unique description", accountType.Description);
    }

    [Fact]
    public async Task GetAccountTypeById_WithNonExistingId_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync("/api/account-type/99999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}

