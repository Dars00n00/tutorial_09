namespace Tutorial9.Services.Warehouses;

public interface IWarehouseService
{
    Task<bool> DoesWarehouseExistAsync(int idWarehouse);
}