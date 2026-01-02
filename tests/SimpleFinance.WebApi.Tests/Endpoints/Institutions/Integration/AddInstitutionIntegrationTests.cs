using System.Net;
using System.Net.Http.Json;
using SimpleFinance.WebApi.Model;
using SimpleFinance.WebApi.Tests.Infrastructure;

namespace SimpleFinance.WebApi.Tests.Endpoints.Institutions.Integration;

public class AddInstitutionIntegrationTests(SimpleFinanceWebApplicationFactory factory)
    : IClassFixture<SimpleFinanceWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task AddInstitution_WithValidModel_ShouldReturnCreated()
    {
        var model = new InstitutionModel { Name = "Test Bank" };

        var response = await _client.PostAsJsonAsync("/api/institution", model);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task AddInstitution_WithValidModel_ShouldReturnLocationHeader()
    {
        var model = new InstitutionModel { Name = "Test Bank" };

        var response = await _client.PostAsJsonAsync("/api/institution", model);

        Assert.NotNull(response.Headers.Location);
        Assert.Contains("/api/institution/", response.Headers.Location.ToString());
    }

    [Fact]
    public async Task AddInstitution_WithValidModel_ShouldReturnCreatedInstitution()
    {
        var model = new InstitutionModel { Name = "Test Bank" };

        var response = await _client.PostAsJsonAsync("/api/institution", model);
        var createdInstitution = await response.Content.ReadFromJsonAsync<InstitutionModel>();

        Assert.NotNull(createdInstitution);
        Assert.Equal(model.Name, createdInstitution.Name);
        Assert.NotNull(createdInstitution.Id);
    }

    [Fact]
    public async Task AddInstitution_WithValidModel_ShouldPersistInstitution()
    {
        var model = new InstitutionModel { Name = "Persisted Bank Unique" };

        var createResponse = await _client.PostAsJsonAsync("/api/institution", model);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        
        var createdInstitution = await createResponse.Content.ReadFromJsonAsync<InstitutionModel>();
        Assert.NotNull(createdInstitution);
        Assert.NotNull(createdInstitution.Id);

        // Use the location header to get the institution
        var locationHeader = createResponse.Headers.Location;
        Assert.NotNull(locationHeader);
        
        var getResponse = await _client.GetAsync(locationHeader);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        
        var retrievedInstitution = await getResponse.Content.ReadFromJsonAsync<InstitutionModel>();
        Assert.NotNull(retrievedInstitution);
        Assert.Equal(model.Name, retrievedInstitution.Name);
    }

    [Fact]
    public async Task AddInstitution_WithEmptyName_ShouldReturnError()
    {
        var model = new InstitutionModel { Name = "" };

        var response = await _client.PostAsJsonAsync("/api/institution", model);

        // Empty name throws ArgumentException in domain object
        Assert.True(response.StatusCode == HttpStatusCode.BadRequest || 
                    response.StatusCode == HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task AddInstitution_WithNullBody_ShouldReturnBadRequest()
    {
        var response = await _client.PostAsJsonAsync<InstitutionModel?>("/api/institution", null);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task AddInstitution_MultipleInstitutions_ShouldCreateSuccessfully()
    {
        var model1 = new InstitutionModel { Name = "Bank One" };
        var model2 = new InstitutionModel { Name = "Bank Two" };

        var response1 = await _client.PostAsJsonAsync("/api/institution", model1);
        var response2 = await _client.PostAsJsonAsync("/api/institution", model2);

        Assert.Equal(HttpStatusCode.Created, response1.StatusCode);
        Assert.Equal(HttpStatusCode.Created, response2.StatusCode);
        
        var institution1 = await response1.Content.ReadFromJsonAsync<InstitutionModel>();
        var institution2 = await response2.Content.ReadFromJsonAsync<InstitutionModel>();

        Assert.NotNull(institution1);
        Assert.NotNull(institution2);
        Assert.Equal("Bank One", institution1.Name);
        Assert.Equal("Bank Two", institution2.Name);
    }

    [Fact]
    public async Task AddInstitution_WithLongName_ShouldReturnCreated()
    {
        var model = new InstitutionModel { Name = new string('A', 100) };

        var response = await _client.PostAsJsonAsync("/api/institution", model);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task AddInstitution_WithSpecialCharactersInName_ShouldReturnCreated()
    {
        var model = new InstitutionModel { Name = "Bank & Trust (Int'l) - Main" };

        var response = await _client.PostAsJsonAsync("/api/institution", model);
        var createdInstitution = await response.Content.ReadFromJsonAsync<InstitutionModel>();

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(createdInstitution);
        Assert.Equal(model.Name, createdInstitution.Name);
    }
}

