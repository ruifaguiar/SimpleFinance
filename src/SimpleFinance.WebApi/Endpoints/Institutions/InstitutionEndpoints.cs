using Microsoft.AspNetCore.Mvc;
using SimpleFinance.WebApi.Endpoints.Institutions.Handlers;
using SimpleFinance.WebApi.Model;

namespace SimpleFinance.WebApi.Endpoints.Institutions;

public static class InstitutionEndpoints
{
    public static IEndpointRouteBuilder MapInstitutionEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/institution",
                ([FromServices] GetAllInstitutionsHandler handler, CancellationToken cancellationToken) =>
                    handler.HandleAsync(cancellationToken))
            .WithName("GetAllInstitutions");
        
        endpoints.MapPost(
                "/api/institution",
                ([FromServices] AddInstitutionHandler handler, [FromBody] InstitutionModel institutionModel, CancellationToken cancellationToken) =>
                    handler.HandleAsync(institutionModel, cancellationToken))
            .WithName("AddInstitution");
        
        endpoints.MapGet("/api/institution/{institutionId:guid}",
                ([FromServices] GetInstitutionByIdHandler handler, [FromRoute] Guid institutionId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(institutionId, cancellationToken))
            .WithName("GetInstitutionById");
        
        endpoints.MapPut("/api/institution/{institutionId:guid}",
                ([FromServices] UpdateInstitutionHandler handler, [FromRoute] Guid institutionId, [FromBody] InstitutionModel institutionModel, CancellationToken cancellationToken) =>
                    handler.HandleAsync(institutionId, institutionModel, cancellationToken))
            .WithName("UpdateInstitution");
                

        return endpoints;
    }
}