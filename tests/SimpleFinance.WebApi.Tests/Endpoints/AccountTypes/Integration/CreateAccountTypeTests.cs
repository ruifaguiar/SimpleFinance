using System.Net;
using System.Net.Http.Json;
using SimpleFinance.WebApi.Model;
using SimpleFinance.WebApi.Tests.Infrastructure;

namespace SimpleFinance.WebApi.Tests.Endpoints.AccountTypes.Integration;

public class CreateAccountTypeTests : IClassFixture<SimpleFinanceWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly SimpleFinanceWebApplicationFactory _factory;

    public CreateAccountTypeTests(SimpleFinanceWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateAccountType_WithValidModel_ShouldReturnCreated()
    {
        var model = new AccountTypeModel { Name = "New Account Type", Description = "Description" };

        var response = await _client.PostAsJsonAsync("/api/account-type", model);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreateAccountType_WithValidModel_ShouldReturnLocationHeader()
    {
        var model = new AccountTypeModel { Name = "Account Type With Location" };

        var response = await _client.PostAsJsonAsync("/api/account-type", model);

        Assert.NotNull(response.Headers.Location);
        Assert.Contains("/api/account-type/", response.Headers.Location.ToString());
    }

    [Fact]
    public async Task CreateAccountType_WithValidModel_ShouldReturnCreatedAccountType()
    {
        var model = new AccountTypeModel { Name = "Created Account Type", Description = "Created description" };

        var response = await _client.PostAsJsonAsync("/api/account-type", model);
        var created = await response.Content.ReadFromJsonAsync<AccountTypeModel>();

        Assert.NotNull(created);
        Assert.NotNull(created.Id);
        Assert.Equal("Created Account Type", created.Name);
        Assert.Equal("Created description", created.Description);
    }

    [Fact]
    public async Task CreateAccountType_WithNullDescription_ShouldSucceed()
    {
        var model = new AccountTypeModel { Name = "No Description Type", Description = null };

        var response = await _client.PostAsJsonAsync("/api/account-type", model);
        var created = await response.Content.ReadFromJsonAsync<AccountTypeModel>();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(created);
        Assert.Null(created.Description);
    }

    [Fact]
    public async Task CreateAccountType_WithEmptyName_ShouldReturnError()
    {
        var model = new AccountTypeModel { Name = "" };

        var response = await _client.PostAsJsonAsync("/api/account-type", model);

        Assert.True(response.StatusCode == HttpStatusCode.BadRequest || 
                    response.StatusCode == HttpStatusCode.InternalServerError);
    }
}

