using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Accounts;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Accounts.Handlers;

public class AddAccountHandler(IAccountService accountService)
{
    public async Task<IResult> HandleAsync(AccountModel model, CancellationToken cancellationToken)
    {
        var account = model.ToDomain();

        var savedAccount = await accountService.AddAccountAsync(account, cancellationToken);

        return Results.CreatedAtRoute("GetAccountById", new { accountId = savedAccount.Id }, savedAccount.ToModel());
    }
}
