using Tutorial9.Repositories.Warehouses;

namespace Tutorial9.Services.Warehouses;

public class WarehousesService
{
    private readonly IWarehousesRepository _warehousesRepository;

    public WarehousesService(IWarehousesRepository warehousesRepository)
    {
        _warehousesRepository = warehousesRepository;
    }
    
    public async Task<bool> DoesWarehouseExistAsync(int idWarehouse)
    {
        return await _warehousesRepository.DoesWarehouseExistAsync(idWarehouse);
    }

}