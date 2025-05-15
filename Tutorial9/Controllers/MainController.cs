using Microsoft.AspNetCore.Mvc;
using Tutorial9.Model;
using Tutorial9.Services.MainService;


namespace Tutorial9.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MainController : ControllerBase
{
    private readonly IMainService _mainService;

    public MainController(IMainService mainService)
    {
        _mainService = mainService;
    }

    [HttpPost]
    public async Task<IActionResult> PostProductWarehouseData(MainOperationDTO data)
    {
        var result = await _mainService.AddProductWarehouseEntry(data);
        return Ok(result);
    }
    
}