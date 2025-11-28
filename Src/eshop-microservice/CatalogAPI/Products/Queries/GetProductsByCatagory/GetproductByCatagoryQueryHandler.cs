using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using Marten;
using Marten.Linq.QueryHandlers;

namespace CatalogAPI.Products.Queries.GetProductsByCatagory
{
    public record GetProductByCategoryQuery(string catagory):IQuery<GetProductByCategoryResult>;   
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetproductByCatagoryQueryHandler (IDocumentSession session, ILogger<GetproductByCatagoryQueryHandler> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductQuery with catagory  @{request}");
            var products=await session.Query<Product>().Where (e=>e.Category.Contains(request.catagory)).
                ToListAsync(cancellationToken);
            return new GetProductByCategoryResult(products);

        }
    }
}
