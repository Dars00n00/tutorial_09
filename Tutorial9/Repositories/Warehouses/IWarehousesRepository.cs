namespace Tutorial9.Repositories.Warehouses;

public interface IWarehousesRepository
{
    Task<bool> DoesWarehouseExistAsync(int idWarehouse);
}