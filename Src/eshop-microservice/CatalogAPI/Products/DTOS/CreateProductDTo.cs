namespace CatalogAPI.Products.DTOS;

public class CreateProductDTo
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public List<string> Category { get; set; } = new List<string>();
    public string ImageFile { get; set; } = default!;
}
