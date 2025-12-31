using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Accounts;

namespace SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;

public class GetAccountTypeByIdHandler(IAccountTypeService accountTypeService)
{
    public async Task<IResult> HandleAsync(int accountTypeId, CancellationToken cancellationToken)
    {
        Domain.DomainObjects.AccountType accountType;

        try
        {
            accountType = await accountTypeService.GetAccountTypeByIdAsync(accountTypeId, cancellationToken);
        }
        catch (NotFoundException)
        {
            return Results.NotFound();
        }

        return Results.Ok(accountType.ToModel());
    }
}

