using Microsoft.AspNetCore.Mvc;
using api.Models.Opc;
using api.Services.Impl;
using OpcLabs.BaseLib.Extensions.Internal;
using api.Exceptions;
using api.Requests;
using api.Core;
using api.Core.api.Core;

namespace api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class OpcController : ControllerBase
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
    public IActionResult BrowseRemote(
      [FromQuery] string host,
      [FromQuery] bool withDa = false
    )
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

    // POST /opc/browseServerItems
    [HttpPost("browseServerItems")]
    public IActionResult BrowseServerItems([FromBody] BrowseServerItemsRequest request)
    {
      try
      {
        var serverItems = service.BrowseServerItems(request);

        return Ok(serverItems);
      }
      catch (OpcItemsBrowsingException ex)
      {
        return BadRequest(ex.GetBaseMessage());
      }
    }

    // GET /opc/serverExists?host={$host}?url={$url}
    [HttpGet("serverExists")]
    public IActionResult ServerExists(
      [FromQuery] string url,
      [FromQuery] string? host = null
    )
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

    // POST /opc/subscribe
    [HttpPost("subscribe")]
    public IActionResult Subscribe([FromBody] SubscriptionRequest request)
    {
      try
      {
        var subscribedIds = service.Subscribe(request);

        return Ok(subscribedIds);
      }
      catch (OpcItemSubscriptionException ex)
      {
        return BadRequest(ex.GetBaseMessage());
      }
    }

    // POST /opc/unsubscribe
    [HttpPost("unsubscribe")]
    public IActionResult Unsubscribe([FromBody] UnsubscriptionRequest request)
    {
      try
      {
        service.Unsubscribe(request);

        return Ok("Successfully unsubscribed");
      }
      catch (OpcItemSubscriptionException ex)
      {
        return BadRequest(ex.GetBaseMessage());
      }
    }

    [Route("/listenSubscribedItems")]
    public async Task ListenSubscribedItems()
    {
      if (HttpContext.WebSockets.IsWebSocketRequest)
      {
        using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
        await WebSocketHandler.HandleWebSocketAsync(webSocket);
      }
      else
      {
        HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
      }
    }
  }
}
