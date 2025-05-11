using Tutorial9.Model;

namespace Tutorial9.Repositories.Products;

public interface IProductsRepository
{
    Task<bool> DoesProductExistAsync(int idProduct);
    Task<ProductDTO> GetProductAsync(int idProduct);
    Task<IEnumerable<ProductDTO>> GetProductsAsync();
}