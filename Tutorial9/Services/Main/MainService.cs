using Microsoft.Data.SqlClient;
using Tutorial9.Model;
using Tutorial9.Repositories.Orders;
using Tutorial9.Repositories.Products;
using Tutorial9.Repositories.Warehouses;
using Tutorial9.Services.Warehouses;

namespace Tutorial9.Services.MainService;

public class MainService : IMainService
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly IWarehousesRepository _warehousesRepository;

    public MainService(
        IOrdersRepository ordersRepository, 
        IProductsRepository productsRepository,
        IWarehousesRepository repository)
    {
        _ordersRepository = ordersRepository;
        _productsRepository = productsRepository;
        _warehousesRepository = repository;
    }
    
    public async Task<int> AddProductWarehouseEntry(MainOperationDTO data)
    {
        //1
        var idProduct = data.IdProduct;
        var doesProductExist = await _productsRepository.DoesProductExistAsync(idProduct);
        if (!doesProductExist)
        {
            throw new Exception($"product {idProduct} does not exist");
        }
        
        var warehouseId = data.IdWarehouse;
        var doesWarehouseExist = await _warehousesRepository.DoesWarehouseExistAsync(warehouseId);
        if (!doesWarehouseExist)
        {
            throw new Exception($"warehouse {warehouseId} does not exist");
        }
        
        var orderAmount = data.Amount;
        if (orderAmount <= 0)
        {
            throw new Exception($"amount {data.Amount} has to be larger than 0");
        }
        
        //2
        var doesOrderExist = await _ordersRepository.DoesOrderExistAsync(idProduct, orderAmount);
        if (!doesOrderExist)
        {
            throw new Exception($"order {idProduct}, {orderAmount} does not exist");
        }
        var order = await _ordersRepository.GetOrderAsync(idProduct, orderAmount);
        if (order.CreatedAt >= data.CreatedAt)
        {
            throw new Exception($"");
        }
        
        //3
        var idOrder = order.IdOrder;
        var isOrderPresentInProductWarehouse 
            = await _ordersRepository.IsOrderFulffiledAsync(idOrder);
        if (isOrderPresentInProductWarehouse)
        {
            throw new Exception($"Order {idOrder} is already in ProductWarehouse");
        }

        await using (SqlConnection conn = new SqlConnection(_warehousesRepository.GetConnectionString()))
        await using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = conn;
            
            await conn.OpenAsync();
            var transaction = await conn.BeginTransactionAsync();
            cmd.Transaction = transaction as SqlTransaction;

            try
            {
                cmd.CommandText = @"UPDATE [Order] 
                                    SET FulfilledAt = @Now 
                                    WHERE IdOrder = @IdOrder";
                
                cmd.Parameters.AddWithValue("@Now", DateTime.Now);
                cmd.Parameters.AddWithValue("@IdOrder", order.IdOrder);
                await cmd.ExecuteNonQueryAsync();

                cmd.Parameters.Clear();
                cmd.CommandText = @"INSERT INTO Product_Warehouse (
                                        IdWarehouse, 
                                        IdProduct, 
                                        IdOrder, 
                                        Amount, 
                                        Price, 
                                        CreatedAt
                                       )
                                    OUTPUT INSERTED.IdProductWarehouse
                                    VALUES (
                                            @IdWarehouse,
                                            @IdProduct, 
                                            @IdOrder, 
                                            @Amount, 
                                            @Price, 
                                            @CreatedAt);";

                var totalPrice = data.Amount * order.IdProduct;

                cmd.Parameters.AddWithValue("@IdWarehouse", warehouseId);
                cmd.Parameters.AddWithValue("@IdProduct", idProduct);
                cmd.Parameters.AddWithValue("@IdOrder", order.IdOrder);
                cmd.Parameters.AddWithValue("@Amount", data.Amount);
                cmd.Parameters.AddWithValue("@Price", totalPrice);
                cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                var insertedId = (int) await cmd.ExecuteScalarAsync();

                await transaction.CommitAsync();
                return insertedId;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
    
}