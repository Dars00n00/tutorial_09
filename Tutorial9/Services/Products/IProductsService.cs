using Tutorial9.Model;

namespace Tutorial9.Services.Products;

public interface IProductsService
{
    Task<bool> DoesProductExistAsync(int idProduct);
    Task<ProductDTO> GetProductAsync(int idProduct);
    Task<IEnumerable<ProductDTO>> GetProductsAsync();
}