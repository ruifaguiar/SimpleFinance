using SimpleFinance.Domain.Exceptions;
using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Institution;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Institutions.Handlers;

public class UpdateInstitutionHandler(IInstitutionService institutionService)
{
    public async Task<IResult> HandleAsync(Guid institutionId, InstitutionModel institutionModel, CancellationToken cancellationToken)
    {
        if (institutionModel.Id != null && institutionModel.Id != institutionId)
        {
            return Results.BadRequest("Institution ID in the URL does not match the ID in the request body.");
        }

        institutionModel.Id = institutionId;
        var institution = institutionModel.ToDomain();
        Domain.DomainObjects.Institution updatedInstitution;
        try
        {
            updatedInstitution = await institutionService.UpdateInstitutionAsync(institution,cancellationToken);
        }
        catch (InstitutionNotFoundException ex)
        {
            return Results.NotFound();
        }

        return Results.Ok(updatedInstitution.ToModel());
    }
}