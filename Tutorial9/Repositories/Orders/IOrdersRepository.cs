using Tutorial9.Model;

namespace Tutorial9.Repositories.Orders;

public interface IOrdersRepository
{
    Task<bool> DoesOrderExistAsync(int idOrder);
    Task<bool> DoesOrderExistAsync(int productId, int amount);
    Task<bool> IsOrderFulffiledAsync(int idOrder);
    Task<OrderDTO> GetOrderAsync(int idOrder);
    Task<OrderDTO> GetOrderAsync(int idProduct, int amount);
    Task<IEnumerable<OrderDTO>> GetOrdersAsync();
}