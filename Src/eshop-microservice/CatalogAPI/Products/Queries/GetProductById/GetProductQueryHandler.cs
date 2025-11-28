using BuildingBlocks.CQRS;
using CatalogAPI.Execptions;
using CatalogAPI.Models;
using Marten;

namespace CatalogAPI.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product );
    internal class GetProductQueryHandler(IDocumentSession session ,ILogger<GetProductQueryHandler> logger) 
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductQuery with @{request}");
            var product=await session.LoadAsync<Product>(request.ProductId,cancellationToken);
            if(product is null)
            {
                throw new ProductNotFoundExecption(request.ProductId);
            }
            return new GetProductByIdResult(product);
        }
    }
}
