
using System.Xml.Linq;
using BuildingBlocks.CQRS;
using CatalogAPI.Products.Queries.GetProducts;

namespace CatalogAPI.Products.Commands.UpdateProduct
{
    public record UpdateProductRequest(Guid id, string name, string description, string imagefile, decimal price, List<string> catagories) : ICommand<UpdateProductResponse>;
    public record UpdateProductResponse(bool IsUpdated);
    public class UpdateProductCommandEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var productCommand = new updateProductCommand(request.id, request.name,
                    request.description,
                    request.imagefile,
                    request.price, request.catagories);
               
                var result = sender.Send(productCommand).Result;
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            }).
                WithName("Update Product")
        .Produces<GetProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
        }
    }
}
