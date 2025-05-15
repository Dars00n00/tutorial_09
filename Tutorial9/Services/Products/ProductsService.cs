using Tutorial9.Model;
using Tutorial9.Repositories.Products;

namespace Tutorial9.Services.Products;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;

    public ProductsService(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<bool> DoesProductExistAsync(int idProduct)
    {
        return await _productsRepository.DoesProductExistAsync(idProduct);
    }

    public async Task<ProductDTO> GetProductAsync(int idProduct)
    {
        if (!await _productsRepository.DoesProductExistAsync(idProduct))
        {
            return null;
        }
        
        return await _productsRepository.GetProductAsync(idProduct);
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        return await _productsRepository.GetProductsAsync();
    }
    
}