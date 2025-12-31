using SimpleFinance.WebApp.Model;

namespace SimpleFinance.WebApp.Gateway;

public class TransactionTypeGateway(HttpClient httpClient)
{
    public async Task<TransactionTypeModel?> CreateTransactionTypeAsync(TransactionTypeModel transactionType)
    {
        var response = await httpClient.PostAsJsonAsync("/api/transaction-type", transactionType);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TransactionTypeModel>();
    }

    public async Task<IEnumerable<TransactionTypeModel>> GetAllTransactionTypesAsync()
    {
        var response = await httpClient.GetAsync("/api/transaction-type");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<TransactionTypeModel>>() ?? [];
    }

    public async Task<TransactionTypeModel?> GetTransactionTypeByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"/api/transaction-type/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TransactionTypeModel>();
    }

    public async Task<TransactionTypeModel?> UpdateTransactionTypeAsync(int id, TransactionTypeModel transactionType)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/transaction-type/{id}", transactionType);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TransactionTypeModel>();
    }

    public async Task<bool> DeleteTransactionTypeAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/transaction-type/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        response.EnsureSuccessStatusCode();
        return true;
    }
}

