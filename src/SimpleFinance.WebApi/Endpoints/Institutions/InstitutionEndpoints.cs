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
        
        endpoints.MapGet("/api/institution/{institutionId:int}",
                ([FromServices] GetInstitutionByIdHandler handler, [FromRoute] int institutionId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(institutionId, cancellationToken))
            .WithName("GetInstitutionById");
        
        endpoints.MapPut("/api/institution/{institutionId:int}",
                ([FromServices] UpdateInstitutionHandler handler, [FromRoute] int institutionId, [FromBody] InstitutionModel institutionModel, CancellationToken cancellationToken) =>
                    handler.HandleAsync(institutionId, institutionModel, cancellationToken))
            .WithName("UpdateInstitution");
        
        endpoints.MapDelete("/api/institution/{institutionId:int}",
                ([FromServices] DeleteInstitutionHandler handler, [FromRoute] int institutionId, CancellationToken cancellationToken) =>
                    handler.HandleAsync(institutionId, cancellationToken))
            .WithName("DeleteInstitution");

        return endpoints;
    }
}