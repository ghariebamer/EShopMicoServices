using CatalogAPI.Products.DTOS;

namespace CatalogAPI.Products.Commands;

public record CreateProductRequest(CreateProductDTo ProductDto);

public record CreateProductResponse(Guid ProductId);
public class CreateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Products", async (CreateProductDTo request, ISender sender) =>
        {
            // map from  CreateProductRequest to CreateProductCommand
            //var command = request.ProductDto.Adapt<CreateProductCommand>();
            var command = new CreateProductCommand(request);
            var result = await sender.Send(command);
            // map result from CreateProductResult to CreateProductResponse
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/Products/{response.ProductId}", response);

        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .WithSummary("Create a new product")
        .WithDescription("Creates a new product in the catalog");
    }
}
