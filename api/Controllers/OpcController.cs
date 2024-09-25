using Microsoft.AspNetCore.Mvc;
using api.Models.Opc;
using api.Services.Impl;
using OpcLabs.BaseLib.Extensions.Internal;
using api.Exceptions;
using System.Text.Json.Nodes;

namespace api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OpcController: ControllerBase
  {
    private static readonly OpcService service = new();

    // GET /opc/browselocal
    [HttpGet("browseLocal")]
    public IActionResult BrowseLocal()
    {
      try
      {
        OpcServer[] servers = service.BrowseServers(true);

        return Ok(servers);
      }
      catch (OpcBrowsingException ex)
      {
        return BadRequest(ex.GetBaseMessage());
      }
    }

    // GET /opc/browseRemote?host={$host}?withDa={bool}
    [HttpGet("browseRemote")]
    public IActionResult BrowseRemote([FromQuery] string host, [FromQuery] bool withDa = false)
    {
      try
      {
        OpcServer[] servers = service.BrowseServers(withDa, host);

        return Ok(servers);
      }
      catch (OpcBrowsingException ex)
      {
        return BadRequest(ex.GetBaseMessage());
      }
    }

    // GET /opc/serverExists?host={$host}?url={$url}
    [HttpGet("serverExists")]
    public IActionResult ServerExists([FromQuery] string url, [FromQuery] string? host = null)
    {
      try
      {
        var result = new { exists = service.ServerExists(url, host) };

        return Ok(result);
      }
      catch (OpcBrowsingException ex)
      {
        return BadRequest(ex.GetBaseMessage());
      }
    }


  }
}
