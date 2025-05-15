using Microsoft.Data.SqlClient;
using Tutorial9.Model;
using Tutorial9.Repositories.Orders;

namespace Tutorial9.Services.Orders;

public class OrdersService : IOrdersService
{
    
    private readonly IOrdersRepository _ordersRepository;

    public OrdersService(IOrdersRepository ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }


    public async Task<bool> DoesOrderExistsAsync(int idOrder)
    {
        return await _ordersRepository.DoesOrderExistAsync(idOrder);
    }

    public async Task<bool> IsOrderFulffiledAsync(int idOrder)
    {
        return await _ordersRepository.IsOrderFulffiledAsync(idOrder);
    }

    public async Task<OrderDTO> GetOrderAsync(int idOrder)
    {
        var doesOrderExists = await DoesOrderExistsAsync(idOrder);
        if (!doesOrderExists)
        {
            throw new Exception($"Order with idOrder {idOrder} does not exist");
        }
        return await _ordersRepository.GetOrderAsync(idOrder);
    }

    public async Task<IEnumerable<OrderDTO>> GetOrdersAsync()
    {
        throw new NotImplementedException();
    }
}