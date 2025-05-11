using Tutorial9.Model;

namespace Tutorial9.Repositories.Orders;

public interface IOrdersRepository
{
    Task<bool> DoesOrderExistAsync(int idOrder);
    Task<OrderDTO> GetOrderAsync(int idOrder);
    Task<IEnumerable<OrderDTO>> GetOrdersAsync();
}