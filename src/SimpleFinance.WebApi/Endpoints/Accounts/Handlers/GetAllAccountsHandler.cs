using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Account;

namespace SimpleFinance.WebApi.Endpoints.Accounts.Handlers;

public class GetAllAccountsHandler(IAccountService accountService)
{
    public async Task<IResult> HandleAsync(CancellationToken cancellationToken)
    {
        var accounts = await accountService.GetAllAccountsAsync(cancellationToken);
        var models = accounts.Select(a => a.ToModel());
        return Results.Ok(models);
    }
}

