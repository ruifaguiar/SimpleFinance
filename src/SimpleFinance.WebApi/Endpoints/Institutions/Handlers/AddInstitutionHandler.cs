using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Institution;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Institutions.Handlers;

public class AddInstitutionHandler(IInstitutionService institutionService)
{
    public async Task<IResult> HandleAsync(InstitutionModel institutionModel, CancellationToken cancellationToken)
    {
        var institution = institutionModel.ToDomain();
        
        var savedInstitution = await institutionService.AddInstitutionAsync(institution, cancellationToken);
       
        return Results.CreatedAtRoute("GetInstitutionById", new { institutionId = savedInstitution.Id }, savedInstitution.ToModel());
    }
}