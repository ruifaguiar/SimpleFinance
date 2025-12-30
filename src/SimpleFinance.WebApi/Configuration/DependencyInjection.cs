using SimpleFinance.WebApi.Endpoints.Institutions.Handlers;

namespace SimpleFinance.WebApi.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddEndpointHandlers(this IServiceCollection services)
    {
        services
            .AddScoped<AddInstitutionHandler>()
            .AddScoped<GetInstitutionByIdHandler>()
            .AddScoped<GetAllInstitutionsHandler>()
            .AddScoped<UpdateInstitutionHandler>();
        
        return services;
    }
}