using Microsoft.Data.SqlClient;

namespace Tutorial9.Repositories.Warehouses;

public class WarehousesRepository : IWarehousesRepository
{
    private readonly string _connectionString;

    public WarehousesRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }
    
    public async Task<bool> DoesWarehouseExistAsync(int idWarehouse)
    {
        string command = @"SELECT * 
                           FROM Warehouse 
                           WHERE IdWarehouse = @IdWarehouse";

        await using (SqlConnection conn = new SqlConnection())
        await using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
            
            await conn.OpenAsync();

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                return await reader.ReadAsync();
            }
        }
    }
    
    public string GetConnectionString()
    {
        return _connectionString;
    }
    
}