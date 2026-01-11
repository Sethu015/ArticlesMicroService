using Blocks.Domain.Abstractions;
using MediatR;

namespace Blocks.Mediatr.Behaviours
{
    public class SetUserIdBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>,IAuditableAction
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            request.CreatedById = 1;
            return await next();
        }
    }
}
