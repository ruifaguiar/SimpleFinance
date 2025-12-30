using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Institution;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Institutions.Handlers;

public class UpdateInstitutionHandler(IInstitutionService institutionService, ILogger<UpdateInstitutionHandler> logger)
{
    public async Task<IResult> HandleAsync(Guid institutionId, InstitutionModel institutionModel,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating institution with ID {InstitutionId}", institutionId);

        if (institutionModel.Id != null && institutionModel.Id != institutionId)
        {
            logger.LogWarning("Institution ID mismatch: URL ID {UrlId} does not match body ID {BodyId}", institutionId,
                institutionModel.Id);
            return Results.BadRequest("Institution ID in the URL does not match the ID in the request body.");
        }

        institutionModel.Id = institutionId;
        var institution = institutionModel.ToDomain();
        Domain.DomainObjects.Institution updatedInstitution;
        try
        {
            updatedInstitution = await institutionService.UpdateInstitutionAsync(institution, cancellationToken);
            logger.LogInformation("Successfully updated institution with ID {InstitutionId}", institutionId);
        }
        catch (NotFoundException)
        {
            logger.LogWarning("Institution with ID {InstitutionId} not found", institutionId);
            return Results.NotFound();
        }

        return Results.Ok(updatedInstitution.ToModel());
    }
}