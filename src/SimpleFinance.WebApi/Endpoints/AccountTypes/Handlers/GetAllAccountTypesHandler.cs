using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Accounts;

namespace SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;

public class GetAllAccountTypesHandler(IAccountTypeService accountTypeService)
{
    public async Task<IResult> HandleAsync(CancellationToken cancellationToken)
    {
        var accountTypes = await accountTypeService.GetAllAccountTypesAsync(cancellationToken);
        var models = accountTypes.Select(a => a.ToModel());
        return Results.Ok(models);
    }
}

