using Microsoft.Extensions.DependencyInjection;
using SimpleFinance.Repository.Implementation;
using SimpleFinance.Repository.Interfaces;

namespace SimpleFinance.Repository.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
       services.AddTransient<IInstitutionRepository, InstitutionRepository>();
       services.AddTransient<IAccountTypeRepository, AccountTypeRepository>();
       services.AddTransient<IAccountRepository, AccountRepository>();

        return services;
    }
}