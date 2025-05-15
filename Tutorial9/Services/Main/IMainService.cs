using Tutorial9.Model;

namespace Tutorial9.Services.MainService;

public interface IMainService
{
    Task<int> AddProductWarehouseEntry(MainOperationDTO data);
}