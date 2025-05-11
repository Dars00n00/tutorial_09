using Microsoft.Data.SqlClient;
using Tutorial9.Model;

namespace Tutorial9.Repositories.Products;

public class ProductsRepository : IProductsRepository
{
    private readonly string _connectionString;

    public ProductsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }
    
    public async Task<bool> DoesProductExistAsync(int idProduct)
    {
        string command = @"SELECT * 
                           FROM Product
                           WHERE IdProduct = @IdProduct";
        
        await using (SqlConnection conn = new SqlConnection(_connectionString))
        await using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            cmd.Parameters.AddWithValue("@IdProduct", idProduct);
            
            await conn.OpenAsync();

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                return await reader.ReadAsync();
            }
        }
    }

    public async Task<ProductDTO> GetProductAsync(int idProduct)
    {
        string command = @"SELECT * 
                           FROM Product 
                           WHERE IdProduct = @IdProduct";
        
        await using (SqlConnection conn = new SqlConnection(_connectionString))
        await using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            cmd.Parameters.AddWithValue("@IdProduct", idProduct);
            
            await conn.OpenAsync();

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                await reader.ReadAsync();
                
                return new ProductDTO
                {
                    IdProduct = reader.GetInt32(reader.GetOrdinal("IdProduct")),
                    Name = reader.GetString(reader.GetOrdinal("Name")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    Price = reader.GetDecimal(reader.GetOrdinal("Price"))
                };
            }
        }
    }

    public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
    {
        throw new NotImplementedException();
    }
}