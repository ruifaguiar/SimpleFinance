using SimpleFinance.WebApp.Model;

namespace SimpleFinance.WebApp.Gateway;

public class TransactionGateway(HttpClient httpClient)
{
    public async Task<TransactionModel?> CreateTransactionAsync(TransactionModel transaction)
    {
        var response = await httpClient.PostAsJsonAsync("/api/transaction", transaction);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TransactionModel>();
    }

    public async Task<IEnumerable<TransactionModel>> GetAllTransactionsAsync()
    {
        var response = await httpClient.GetAsync("/api/transaction");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<TransactionModel>>() ?? [];
    }

    public async Task<TransactionModel?> GetTransactionByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"/api/transaction/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TransactionModel>();
    }

    public async Task<IEnumerable<TransactionModel>> GetTransactionsByAccountIdAsync(int accountId)
    {
        var response = await httpClient.GetAsync($"/api/account/{accountId}/transactions");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<TransactionModel>>() ?? [];
    }

    public async Task<TransactionModel?> UpdateTransactionAsync(int id, TransactionModel transaction)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/transaction/{id}", transaction);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TransactionModel>();
    }

    public async Task<bool> DeleteTransactionAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/transaction/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        response.EnsureSuccessStatusCode();
        return true;
    }
}

