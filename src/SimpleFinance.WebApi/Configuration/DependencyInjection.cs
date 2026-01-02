using SimpleFinance.WebApi.Endpoints.Institutions.Handlers;
using SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;
using SimpleFinance.WebApi.Endpoints.Accounts.Handlers;
using SimpleFinance.WebApi.Endpoints.ExpenseCategories.Handlers;
using SimpleFinance.WebApi.Endpoints.Transactions.Handlers;
using SimpleFinance.WebApi.Endpoints.TransactionTypes.Handlers;

namespace SimpleFinance.WebApi.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddEndpointHandlers(this IServiceCollection services)
    {
        services
            .AddScoped<AddInstitutionHandler>()
            .AddScoped<GetInstitutionByIdHandler>()
            .AddScoped<GetAllInstitutionsHandler>()
            .AddScoped<UpdateInstitutionHandler>()
            .AddScoped<AddAccountTypeHandler>()
            .AddScoped<GetAccountTypeByIdHandler>()
            .AddScoped<GetAllAccountTypesHandler>()
            .AddScoped<UpdateAccountTypeHandler>()
            .AddScoped<DeleteAccountTypeHandler>()
            .AddScoped<AddAccountHandler>()
            .AddScoped<GetAccountByIdHandler>()
            .AddScoped<GetAllAccountsHandler>()
            .AddScoped<UpdateAccountHandler>()
            .AddScoped<DeleteAccountHandler>()
            .AddScoped<AddExpenseCategoryHandler>()
            .AddScoped<GetExpenseCategoryByIdHandler>()
            .AddScoped<GetAllExpenseCategoriesHandler>()
            .AddScoped<UpdateExpenseCategoryHandler>()
            .AddScoped<DeleteExpenseCategoryHandler>()
            .AddScoped<AddTransactionHandler>()
            .AddScoped<GetTransactionByIdHandler>()
            .AddScoped<GetAllTransactionsHandler>()
            .AddScoped<GetTransactionsByAccountIdHandler>()
            .AddScoped<UpdateTransactionHandler>()
            .AddScoped<DeleteTransactionHandler>()
            .AddScoped<AddTransactionTypeHandler>()
            .AddScoped<GetTransactionTypeByIdHandler>()
            .AddScoped<GetAllTransactionTypesHandler>()
            .AddScoped<UpdateTransactionTypeHandler>()
            .AddScoped<DeleteTransactionTypeHandler>()
            .AddScoped<DeleteInstitutionHandler>();
        
        return services;
    }
}