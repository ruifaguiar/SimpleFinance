using System.Net;
using System.Net.Http.Json;
using SimpleFinance.WebApi.Model;
using SimpleFinance.WebApi.Tests.Infrastructure;

namespace SimpleFinance.WebApi.Tests.Endpoints.AccountTypes.Integration;

public class GetAllAccountTypesTests(SimpleFinanceWebApplicationFactory factory)
    : IClassFixture<SimpleFinanceWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();
    private readonly SimpleFinanceWebApplicationFactory _factory = factory;

    [Fact]
    public async Task GetAllAccountTypes_ShouldReturnOk()
    {
        var response = await _client.GetAsync("/api/account-type");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAllAccountTypes_ShouldReturnListOfAccountTypes()
    {
        // Add some account types first
        await _client.PostAsJsonAsync("/api/account-type", new AccountTypeModel { Name = "Savings" });
        await _client.PostAsJsonAsync("/api/account-type", new AccountTypeModel { Name = "Checking" });

        var response = await _client.GetAsync("/api/account-type");
        var accountTypes = await response.Content.ReadFromJsonAsync<List<AccountTypeModel>>();

        Assert.NotNull(accountTypes);
        Assert.True(accountTypes.Count >= 2);
    }
}

