using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Articles.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Submission.Application.Features.UploadFile;

namespace Submission.API.Endpoints
{
    public class UploadManuscriptFileEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/articles/{articleId:int}/assets/manuscript:upload",
                async ([FromRoute] int articleId, [FromForm] UploadManuscriptCommand command, ISender sender) =>
                {
                    var response = await sender.Send(command with { ArticleId = articleId });
                    return Results.Created($"/api/articles/{articleId}/assets/{response.Id}:download", response);
                })
                .RequireRoleAuthorization(Role.AUT)
                .WithName("UploadManuscript")
                .WithTags("Assets")
                .Produces<IdResponse>(StatusCodes.Status201Created)
                .ProducesValidationProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status401Unauthorized)
                .DisableAntiforgery(); // because of IFormFile
        }
    }
}
