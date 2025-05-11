using Tutorial9.Model;

namespace Tutorial9.Services.Products;

public interface IProductsService
{
    Task<ProductDTO> GetProductAsync(int idProduct);
}