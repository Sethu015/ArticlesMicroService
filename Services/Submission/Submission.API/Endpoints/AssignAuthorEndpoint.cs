using Articles.Abstractions;
using Articles.Abstractions.Enums;
using MediatR;
using Articles.Security;
using Submission.Application.Features.AssignAuthor;

namespace Submission.API.Endpoints;

public static class AssignAuthorEndpoint
{
    public static void Map(this IEndpointRouteBuilder app) 
    {
        app.MapPut("/api/articles/{articleId:int}/authors/{authorId:int}", async (int articleId, int authorId, AssignAuthorCommand assignAuthorCommand,ISender sender) =>
        {
            var response = await sender.Send(assignAuthorCommand with { ArticleId = articleId, AuthorId = authorId});
            return Results.Ok(response);
        }
        )
        .RequireRoleAuthorization(Role.CORAUT)
        .WithName("AssignAuthor")
        .WithTags("Articles")
        .Produces<IdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest);
    }
}
