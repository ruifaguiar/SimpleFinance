using SimpleFinance.Services.Interfaces;
using SimpleFinance.WebApi.Mappings.Institution;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Institution.Handlers;

public class AddInstitutionHandler(IInstitutionService institutionService)
{
    public async Task<IResult> HandleAsync(InstitutionModel institutionModel)
    {
        var institution = institutionModel.ToDomain();
        
        var savedInstitution = await institutionService.AddInstitutionAsync(institution);
       
        return Results.CreatedAtRoute("GetInstitutionById", new { institutionId = savedInstitution.Id }, savedInstitution.ToModel());
    }
}