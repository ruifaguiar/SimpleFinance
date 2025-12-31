using SimpleFinance.WebApp.Model;

namespace SimpleFinance.WebApp.Gateway;

public class ExpenseCategoryGateway(HttpClient httpClient)
{
    public async Task<ExpenseCategoryModel?> CreateExpenseCategoryAsync(ExpenseCategoryModel expenseCategory)
    {
        var response = await httpClient.PostAsJsonAsync("/api/expense-category", expenseCategory);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ExpenseCategoryModel>();
    }

    public async Task<IEnumerable<ExpenseCategoryModel>> GetAllExpenseCategoriesAsync()
    {
        var response = await httpClient.GetAsync("/api/expense-category");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<ExpenseCategoryModel>>() ?? [];
    }

    public async Task<ExpenseCategoryModel?> GetExpenseCategoryByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"/api/expense-category/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ExpenseCategoryModel>();
    }

    public async Task<ExpenseCategoryModel?> UpdateExpenseCategoryAsync(int id, ExpenseCategoryModel expenseCategory)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/expense-category/{id}", expenseCategory);
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ExpenseCategoryModel>();
    }

    public async Task<bool> DeleteExpenseCategoryAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/expense-category/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }
        response.EnsureSuccessStatusCode();
        return true;
    }
}

