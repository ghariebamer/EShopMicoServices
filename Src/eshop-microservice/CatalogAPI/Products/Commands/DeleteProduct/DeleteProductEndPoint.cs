
using CatalogAPI.Products.Queries.GetProducts;

namespace CatalogAPI.Products.Commands.DeleteProduct
{
    public record DeleteProductResponse(bool IsDeleted);
    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Products/{productId:guid}", async(Guid productId, ISender sender) =>{

                var result=await sender.Send(new DeleteProductCommand(productId));
                var response=result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            }).WithName("Delete Product")
        .Produces<GetProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product");
        }
    }
}
