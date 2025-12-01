using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using CatalogAPI.Products.DTOS;
using Marten;

namespace CatalogAPI.Products.Commands;

public record CreateProductCommand(CreateProductDTo ProductDto) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid ProductId);
public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
{    public CreateProductCommandValidation()
    {
            RuleFor(X=>X.ProductDto.Name).NotEmpty().WithMessage("Product name is required");
            RuleFor(X=>X.ProductDto.Category).NotEmpty().WithMessage("Product Category is required");
            RuleFor(x=>x.ProductDto.ImageFile).NotEmpty().WithMessage("Product ImageFile is required");
            RuleFor(x=>x.ProductDto.Price).GreaterThan(0).WithMessage("Product Price must be greater than zero");
    }
}
public class CreateProductHandler(IDocumentSession session,IValidator<CreateProductCommand> validator) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // vaild data before processing
        var validationResult = validator.ValidateAsync(command,cancellationToken);
        var errors = validationResult.Result.Errors.Select(x=>x.ErrorMessage).ToList();
        if (errors.Any())
        {
            throw new ValidationException(errors.FirstOrDefault());
        }
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
