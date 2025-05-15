using Tutorial9.Model;

namespace Tutorial9.Services.Orders;

public interface IOrdersService
{
    Task<bool> DoesOrderExistsAsync(int idOrder);
    Task<OrderDTO> GetOrderAsync(int idOrder);
    Task<IEnumerable<OrderDTO>> GetOrdersAsync();
}