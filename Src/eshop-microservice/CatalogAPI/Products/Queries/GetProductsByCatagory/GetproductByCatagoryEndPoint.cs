using CatalogAPI.Models;
using CatalogAPI.Products.Queries.GetProducts;

namespace CatalogAPI.Products.Queries.GetProductsByCatagory
{
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetproductByCatagoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Products/catagory/{catagory}", async(string catagory,ISender sender) =>
            {
                var result=await sender.Send(new GetProductByCategoryQuery(catagory));
                var response=result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            }).WithName("GetProductbycatagory")
        .Produces<GetProductResponse>(StatusCodes.Status200OK)
        .WithSummary("GetProductbycatagory")
        .WithDescription("GetProductbycatagory");
        }
    }
}
