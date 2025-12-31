using SimpleFinance.WebApp.Gateway;

namespace SimpleFinance.WebApp.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        var baseAddress = new Uri(configuration["ApiBaseUrl"] ?? "https://localhost:5001");
        
        services.AddHttpClient<InstitutionGateway>(client =>
        {
            client.BaseAddress = baseAddress;
        });
        
        services.AddHttpClient<AccountTypeGateway>(client =>
        {
            client.BaseAddress = baseAddress;
        });

        services.AddHttpClient<AccountGateway>(client =>
        {
            client.BaseAddress = baseAddress;
        });

        services.AddHttpClient<ExpenseCategoryGateway>(client =>
        {
            client.BaseAddress = baseAddress;
        });

        services.AddHttpClient<TransactionGateway>(client =>
        {
            client.BaseAddress = baseAddress;
        });
        
        return services;
    }
}