using SimpleFinance.WebApp.Model;

namespace SimpleFinance.WebApp.Gateway;

public class InstitutionGateway(HttpClient httpClient)
{
    public async Task<InstitutionModel?> CreateInstitutionAsync(InstitutionModel institution)
    {
        var response = await httpClient.PostAsJsonAsync("/api/institution", institution);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InstitutionModel>();
    }
    
    public async Task<IEnumerable<InstitutionModel>> GetAllInstitutionsAsync()
    {
        var response = await httpClient.GetAsync("/api/institution");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<InstitutionModel>>() ?? [];
    }
}