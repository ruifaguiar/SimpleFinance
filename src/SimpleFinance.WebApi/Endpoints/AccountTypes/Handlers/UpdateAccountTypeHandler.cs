using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Accounts;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.AccountTypes.Handlers;

public class UpdateAccountTypeHandler(IAccountTypeService accountTypeService, ILogger<UpdateAccountTypeHandler> logger)
{
    public async Task<IResult> HandleAsync(int accountTypeId, AccountTypeModel model, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating account type with ID {AccountTypeId}", accountTypeId);

        if (model.Id != null && model.Id != accountTypeId)
        {
            logger.LogWarning("AccountType ID mismatch: URL ID {UrlId} does not match body ID {BodyId}", accountTypeId, model.Id);
            return Results.BadRequest("AccountType ID in the URL does not match the ID in the request body.");
        }

        model.Id = accountTypeId;
        var accountType = model.ToDomain();
        Domain.DomainObjects.AccountType updatedAccountType;

        try
        {
            updatedAccountType = await accountTypeService.UpdateAccountTypeAsync(accountType, cancellationToken);
            logger.LogInformation("Successfully updated account type with ID {AccountTypeId}", accountTypeId);
        }
        catch (NotFoundException)
        {
            logger.LogWarning("AccountType with ID {AccountTypeId} not found", accountTypeId);
            return Results.NotFound();
        }

        return Results.Ok(updatedAccountType.ToModel());
    }
}

