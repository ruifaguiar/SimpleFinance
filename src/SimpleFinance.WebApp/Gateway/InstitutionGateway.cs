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
    
    public async Task<InstitutionModel?> GetInstitutionByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"/api/institution/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InstitutionModel>();
    }
    
    public async Task<InstitutionModel?> UpdateInstitutionAsync(int id, InstitutionModel institution)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/institution/{id}", institution);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<InstitutionModel>();
    }
    
    public async Task<bool> DeleteInstitutionAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/institution/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        response.EnsureSuccessStatusCode();
        return true;
    }
}