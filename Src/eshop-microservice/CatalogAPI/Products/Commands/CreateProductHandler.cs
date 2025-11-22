using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using CatalogAPI.Products.DTOS;
using Marten;

namespace CatalogAPI.Products.Commands;

public record CreateProductCommand(CreateProductDTo ProductDto) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid ProductId);
public class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // write logic to create product
        // create a new product in the database using the data from command.ProductDto
        Product product = new Product
        {
            Id = Guid.NewGuid(),
            Name = command.ProductDto.Name,
            Description = command.ProductDto.Description,
            Price = command.ProductDto.Price,
            Category = command.ProductDto.Category,
            ImageFile = command.ProductDto.ImageFile
        };

        // Save the product to the database (pseudo-code)
        session.Store(product);
        session.SaveChangesAsync();

        // return the result
        return Task.FromResult(new CreateProductResult(product.Id));
        throw new NotImplementedException();
    }
}
