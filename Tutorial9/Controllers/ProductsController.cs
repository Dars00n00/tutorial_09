using Microsoft.AspNetCore.Mvc;
using Tutorial9.Services.Products;

namespace Tutorial9.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var res = await _productsService.GetProductAsync(id);
        if (res == null)
        {
            return NotFound();
        }
        return Ok(res);
    }

    /*public async Task<bool> AddProductAsync()
    {
        
    }
    
    
    public async Task<bool> DoesProductExist()
    {
        var doesExist = await _iProductsService.DoesProductExistAsync();
    }*/
}