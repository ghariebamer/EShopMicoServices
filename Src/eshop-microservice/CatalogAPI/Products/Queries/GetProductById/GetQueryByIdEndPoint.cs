using CatalogAPI.Models;
using CatalogAPI.Products.Queries.GetProducts;

namespace CatalogAPI.Products.Queries.GetProductById
{
    public record GetProductByIdResponse(Product Product);
    public class GetQueryByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products/{productId:guid}", async (Guid productId, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(productId));
                var response =result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            }).WithName("GetProductbyId")
        .Produces<GetProductResponse>(StatusCodes.Status200OK)
        .WithSummary("GetProductbyId")
        .WithDescription("GetProductbyId");

        }
    }
}
