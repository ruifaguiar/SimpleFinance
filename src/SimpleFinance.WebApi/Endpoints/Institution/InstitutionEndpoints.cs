using Microsoft.AspNetCore.Mvc;
using SimpleFinance.WebApi.Endpoints.Institution.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Institution;

public static class InstitutionEndpoints
{
    public static IEndpointRouteBuilder MapInstitutionEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/institution",
                ([FromServices] GetAllInstitutionsHandler handler) =>
                    handler.HandleAsync())
            .WithName("GetAllInstitutions");
        
        endpoints.MapPost(
                "/api/institution",
                ([FromServices] AddInstitutionHandler handler, [FromBody] InstitutionModel institutionModel) =>
                    handler.HandleAsync(institutionModel))
            .WithName("AddInstitution");
        
        endpoints.MapGet("/api/institution/{institutionId:guid}",
                ([FromServices] GetInstitutionByIdHandler handler, [FromRoute] Guid institutionId) =>
                    handler.HandleAsync(institutionId))
            .WithName("GetInstitutionById");
                

        return endpoints;
    }
}