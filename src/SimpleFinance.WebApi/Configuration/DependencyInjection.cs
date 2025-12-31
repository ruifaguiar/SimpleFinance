using SimpleFinance.WebApi.Endpoints.Institutions.Handlers;
using SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;

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
            .AddScoped<DeleteAccountTypeHandler>();
        
        return services;
    }
}