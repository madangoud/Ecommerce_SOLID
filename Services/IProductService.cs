using ECommerceAPI.Models;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
    Task<Product> CreateProductAsync(Product product);
}