using BinixPro.Database.Models;
using BinixPro.Database.Services;
using Microsoft.AspNetCore.Mvc;

namespace BinixPro.Node.Controllers;
[ApiController]
[Route("[controller]")]
public class RouteController : ControllerBase
{
    private readonly ILogger<RouteController> _logger;
    private readonly IProxyService _proxyService;

    public RouteController(ILogger<RouteController> logger, IProxyService proxyService)
    {
        _logger = logger;
        _proxyService = proxyService;
    }
    
    [HttpPost]
    public async Task<ActionResult<Database.Models.Route.Response>> Post([FromBody] Database.Models.Route.Request model)
    {
        return Ok(await _proxyService.CreateRouteAsync(model));
    }
}
