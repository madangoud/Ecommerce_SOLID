using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Models;
using ECommerceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ECommerceAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
   // [Authorize]
    public  async Task<IActionResult> Get()
    {
            var products = await _service.GetProductsAsync();
            return Ok(products);
    }

    [HttpPost]
    //[Authorize]
    public async Task<IActionResult> Create(Product product)
    {
        var createdProduct = await _service.CreateProductAsync(product);
        return Ok(createdProduct);
    }
}