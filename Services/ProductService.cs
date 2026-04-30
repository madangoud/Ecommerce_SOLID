using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerceAPI.Services;



public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMemoryCache _cache;


   public ProductService(AppDbContext context, IMemoryCache cache)
{
    _context = context;
    _cache = cache;
}

    public async Task<List<Product>> GetProductsAsync()
{
    if (!_cache.TryGetValue("products", out List<Product> products))
    {
        products = await _context.Products.ToListAsync();

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        _cache.Set("products", products, cacheOptions);
    }

    return products;
}
    public async Task<Product> CreateProductAsync(Product product)
{
    await _context.Products.AddAsync(product);
    await _context.SaveChangesAsync();

    _cache.Remove("products"); // 🔥 invalidate cache

    return product;
}
}