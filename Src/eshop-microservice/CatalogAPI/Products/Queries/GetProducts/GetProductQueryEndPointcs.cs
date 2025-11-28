
using CatalogAPI.Models;
using CatalogAPI.Products.Commands;

namespace CatalogAPI.Products.Queries.GetProducts
{
    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductQueryEndPointcs : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products", async (ISender sender) =>
            {
                var query = new GetProductQuery();
                var result = await sender.Send(query);
                GetProductResponse response=result.Adapt<GetProductResponse>();
                return Results.Ok(result);
            })
        .WithName("GetProducts")
        .Produces<GetProductResponse>(StatusCodes.Status200OK)
        .WithSummary("Get all producst")
        .WithDescription("Get all  product in the catalog");
        
    }
    }
}
