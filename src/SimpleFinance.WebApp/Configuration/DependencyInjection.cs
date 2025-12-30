using SimpleFinance.WebApp.Gateway;

namespace SimpleFinance.WebApp.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<InstitutionGateway>(client =>
        {
            client.BaseAddress = new Uri(configuration["ApiBaseUrl"] ?? "https://localhost:5001");
        });
        
        return services;
    }
}