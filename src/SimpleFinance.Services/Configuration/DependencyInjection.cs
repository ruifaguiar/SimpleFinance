using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleFinance.Services.Implementation;
using SimpleFinance.Services.Interfaces;

namespace SimpleFinance.Services.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
       services.AddTransient<IInstitutionService, InstitutionService>();
       services.AddTransient<IAccountTypeService, AccountTypeService>();

        return services;
    }
}