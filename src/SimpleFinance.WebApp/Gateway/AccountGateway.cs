using SimpleFinance.WebApp.Model;

namespace SimpleFinance.WebApp.Gateway;

public class AccountGateway(HttpClient httpClient)
{
    public async Task<AccountModel?> CreateAccountAsync(AccountModel account)
    {
        var response = await httpClient.PostAsJsonAsync("/api/account", account);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AccountModel>();
    }

    public async Task<IEnumerable<AccountModel>> GetAllAccountsAsync()
    {
        var response = await httpClient.GetAsync("/api/account");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<AccountModel>>() ?? [];
    }

    public async Task<AccountModel?> GetAccountByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"/api/account/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AccountModel>();
    }

    public async Task<AccountModel?> UpdateAccountAsync(int id, AccountModel account)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/account/{id}", account);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<AccountModel>();
    }

    public async Task<bool> DeleteAccountAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/account/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        response.EnsureSuccessStatusCode();
        return true;
    }
}

