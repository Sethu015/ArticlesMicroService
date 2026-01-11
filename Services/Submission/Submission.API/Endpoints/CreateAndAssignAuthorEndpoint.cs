using Articles.Abstractions;
using Articles.Abstractions.Enums;
using Articles.Security;
using MediatR;
using Submission.Application.Features.CreateAndAssignAuthor;

namespace Submission.API.Endpoints
{
    public static class CreateAndAssignAuthorEndpoint
    {
        public static void Map(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapPost("api/articles/{articleId:int}/authors", async (int articleId, CreateAndAssignAuthorCommand command, ISender sender) =>
            {
                var response = await sender.Send(command with { ArticleId = articleId });
                return Results.Ok(response);
            })
            .RequireRoleAuthorization(UserRoleType.CORAUT)
            .WithName("CreateAndAssignAuthor")
            .Produces<IdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesValidationProblem(StatusCodes.Status400BadRequest);
        }
    }
}
