using Microsoft.AspNetCore.Mvc;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.OperationModel;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.Discovery;
using OpcLabs.EasyOpc.UA.OperationModel;
using api.Models.Opc;

namespace api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OpcController: ControllerBase
  {
    private static readonly EasyDAClient daClient = new EasyDAClient();
    private static readonly EasyUAClient uaClient = new EasyUAClient();

    [HttpGet("Da/BrowseLocal")]
    public IActionResult browseLocalDa()
    {
      try
      {
        var serverElementCollection = daClient.BrowseServers("");
        var servers = serverElementCollection
          .Select(server => new OpcDa(
            server.Description,
            server.ProgId,
            server.UrlString))
          .ToList();

        return Ok(servers);
      }
      catch (OpcException daException)
      {
        return BadRequest(new { error = daException.GetBaseException().Message });
      }
    }

    [HttpGet("Ua/BrowseLocal")]
    public IActionResult browseLocalUa()
    {
      try
      {
        var serverElementCollection = uaClient.DiscoverLocalServers("opcua.demo-this.com");
        var servers = serverElementCollection
          .Select(server => new OpcUa(
            server.ApplicationName,
            server.DiscoveryUriString,
            server.ApplicationUriString))
          .ToList();

        return Ok(servers);
      }
      catch (OpcException daException)
      {
        return BadRequest(new { error = daException.GetBaseException().Message });
      }
    }
  }
}
