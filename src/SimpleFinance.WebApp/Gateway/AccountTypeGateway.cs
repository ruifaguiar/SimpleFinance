using SimpleFinance.WebApp.Model;

namespace SimpleFinance.WebApp.Gateway;

public class AccountTypeGateway(HttpClient httpClient)
{
    public async Task<AccountTypeModel?> CreateAccountTypeAsync(AccountTypeModel accountType)
    {
        var response = await httpClient.PostAsJsonAsync("/api/account-type", accountType);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AccountTypeModel>();
    }

    public async Task<IEnumerable<AccountTypeModel>> GetAllAccountTypesAsync()
    {
        var response = await httpClient.GetAsync("/api/account-type");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<AccountTypeModel>>() ?? [];
    }

    public async Task<AccountTypeModel?> GetAccountTypeByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"/api/account-type/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AccountTypeModel>();
    }

    public async Task<AccountTypeModel?> UpdateAccountTypeAsync(int id, AccountTypeModel accountType)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/account-type/{id}", accountType);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AccountTypeModel>();
    }

    public async Task<bool> DeleteAccountTypeAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/account-type/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        response.EnsureSuccessStatusCode();
        return true;
    }
}

