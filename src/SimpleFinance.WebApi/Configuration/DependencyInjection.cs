using SimpleFinance.WebApi.Endpoints.Institution.Handlers;

namespace SimpleFinance.WebApi.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddEndpointHandlers(this IServiceCollection services)
    {
        services.AddScoped<AddInstitutionHandler>();
        return services;
    }
}