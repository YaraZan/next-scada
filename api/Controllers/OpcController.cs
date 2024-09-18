using Microsoft.AspNetCore.Mvc;
using api.Models.Opc;
using api.Services.Impl;
using OpcLabs.BaseLib.Extensions.Internal;

namespace api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OpcController: ControllerBase
  {
    private static readonly OpcService service = new();

    // GET /opc/browselocal
    [HttpGet("/browselocal")]
    public IActionResult BrowseLocal()
    {
      try
      {
        OpcServer[] servers = service.BrowseServers(true);

        return Ok(servers);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.GetBaseMessage());
      }
    }
  }
}
