using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Accounts;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;

public class AddAccountTypeHandler(IAccountTypeService accountTypeService)
{
    public async Task<IResult> HandleAsync(AccountTypeModel model, CancellationToken cancellationToken)
    {
        var accountType = model.ToDomain();

        var savedAccountType = await accountTypeService.AddAccountTypeAsync(accountType, cancellationToken);

        return Results.CreatedAtRoute("GetAccountTypeById", new { accountTypeId = savedAccountType.Id }, savedAccountType.ToModel());
    }
}
