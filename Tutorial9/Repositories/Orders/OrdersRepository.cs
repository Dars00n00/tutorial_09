using Microsoft.Data.SqlClient;
using Tutorial9.Model;


namespace Tutorial9.Repositories.Orders;


public class OrdersRepository : IOrdersRepository
{
    private readonly string _connectionString;

    public OrdersRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<bool> DoesOrderExistAsync(int idOrder)
    {
        string command = @"SELECT * 
                            FROM Order 
                            WHERE OrderID = @idOrder";
        
        await using (SqlConnection conn = new SqlConnection(_connectionString))
        await using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            cmd.Parameters.AddWithValue("@idOrder", idOrder);
            
            await conn.OpenAsync();

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                return await reader.ReadAsync();
            }
        }
    }

    public async Task<bool> DoesOrderExistAsync(int idProduct, int amount)
    {
        string command = @"SELECT * 
                           FROM Order 
                           WHERE IdProduct = @idProduct 
                           AND Amount = @amount";
        
        await using (SqlConnection conn = new SqlConnection(_connectionString))
        await using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            cmd.Parameters.AddWithValue("@IdProduct", idProduct);
            cmd.Parameters.AddWithValue("@amount", amount);
            
            await conn.OpenAsync();

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                return await reader.ReadAsync();
            }
        }
    }

    public async Task<bool> IsOrderFulffiledAsync(int idOrder)
    {
        string command = @"SELECT * 
                           FROM Product_Warehouse 
                           WHERE IdOrder = @IdOrder";

        await using (SqlConnection conn = new SqlConnection(_connectionString))
        await using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            cmd.Parameters.AddWithValue("@IdOrder", idOrder);

            await conn.OpenAsync();

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                return await reader.ReadAsync();
            }
        }
    }

    public async Task<OrderDTO> GetOrderAsync(int idOrder)
    {
        string command = @"SELECT * 
                           FROM Order
                           WHERE OrderID = @idOrder";

        await using (SqlConnection conn = new SqlConnection(_connectionString))
        await using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            cmd.Parameters.AddWithValue("@idOrder", idOrder);
            
            await conn.OpenAsync();

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                await reader.ReadAsync();
                
                return new OrderDTO
                {
                    IdOrder = reader.GetInt32(reader.GetOrdinal("IdOrder")),
                    IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct")),
                    Amount = reader.GetInt32(reader.GetOrdinal("Amount")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    FulfilledAt = reader.IsDBNull(reader.GetOrdinal("FulfilledAt"))
                        ? null : reader.GetDateTime(reader.GetOrdinal("FulfilledAt"))
                };
            }
        }
        
    }

    public async Task<OrderDTO> GetOrderAsync(int idProduct, int amount)
    {
        string command = @"SELECT * 
                           FROM Order 
                           WHERE IdProduct = @idProduct
                           AND Amount = @amount";

        await using (SqlConnection conn = new SqlConnection(_connectionString))
        await using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            cmd.Parameters.AddWithValue("@IdProduct", idProduct);
            cmd.Parameters.AddWithValue("@amount", amount);
            
            await conn.OpenAsync();

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                await reader.ReadAsync();
                
                return new OrderDTO
                {
                    IdOrder = reader.GetInt32(reader.GetOrdinal("IdOrder")),
                    IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct")),
                    Amount = reader.GetInt32(reader.GetOrdinal("Amount")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    FulfilledAt = reader.IsDBNull(reader.GetOrdinal("FulfilledAt"))
                        ? null : reader.GetDateTime(reader.GetOrdinal("FulfilledAt"))
                };
            }
        }
    }

    public Task<IEnumerable<OrderDTO>> GetOrdersAsync()
    {
        throw new NotImplementedException();
    }
}