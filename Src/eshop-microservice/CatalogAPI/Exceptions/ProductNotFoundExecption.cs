namespace CatalogAPI.Execptions
{
    public class ProductNotFoundExecption : Exception
    {
        public ProductNotFoundExecption(Guid productID):base($"Product with this id {{productID}} not found.")
        {
        }
    }
}
