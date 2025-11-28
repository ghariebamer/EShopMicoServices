using BuildingBlocks.CQRS;
using CatalogAPI.Execptions;
using CatalogAPI.Models;
using CatalogAPI.Products.DTOS;
using CatalogAPI.Products.Queries.GetProductsByCatagory;
using Marten;

namespace CatalogAPI.Products.Commands.UpdateProduct
{
    public record updateProductCommand(Guid id,string name , string description ,string imagefile, decimal price,List<string> catagories ) : ICommand<updateProductResult>;
    public record updateProductResult(bool IsUpdated);
    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<GetproductByCatagoryQueryHandler> logger) : 
        ICommandHandler<updateProductCommand, updateProductResult>
    {
        public async Task<updateProductResult> Handle(updateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling updating product  @{request}");
            var product= session.LoadAsync<Product>(command.id,cancellationToken).Result;
            if(product is null)
            {
                throw new ProductNotFoundExecption(command.id);
            }
            else
            {
                product.Name= command.name;
                product.Description= command.description;
                product.ImageFile= command.imagefile;
                product.Price= command.price;
                product.Category= command.catagories;
            }
            session.Update(product);
            session.Update(product); 
            return new updateProductResult(true);

        }
    }
}
