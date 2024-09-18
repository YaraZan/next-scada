using api.Models.Opc;
using api.Services.Meta;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.OperationModel;
using OpcLabs.EasyOpc.UA;

namespace api.Services.Impl
{
  public class OpcService : OpcServiceMeta
  {
    private static readonly EasyDAClient daClient = new();
    private static readonly EasyUAClient uaClient = new();

    public override OpcDa[] BrowseDaServers(string? host = null)
    {
      var serverElementCollection = daClient.BrowseServers(host ?? "");
      var servers = serverElementCollection
        .Select(server => new OpcDa(
          server.Description,
          server.ProgId,
          server.UrlString,
          host))
        .ToArray();

      return servers;
    }

    public override OpcUa[] BrowseUaServers(string? host = null)
    {
      var serverElementCollection = uaClient.DiscoverLocalServers(host ?? "opcua.demo-this.com");
      var servers = serverElementCollection
        .Select(server => new OpcUa(
          server.ApplicationName,
          server.DiscoveryUriString,
          server.ApplicationUriString,
          host))
        .ToArray();

      return servers;
    }

    public override OpcServer[] BrowseServers(bool withDa = false, string? host = null)
    {
      List<OpcServer> opcServers = [];
      List<string> errors = [];

      if (withDa)
      {
        try
        {
          var daServers = BrowseDaServers(host);
          opcServers.AddRange(daServers.Cast<OpcServer>());
        }
        catch (Exception daException)
        {
          errors.Add($"DA Error: {daException.Message}");
        }
      }

      try
      {
          var uaServers = BrowseUaServers(host);
          opcServers.AddRange(uaServers.Cast<OpcServer>());
      }
      catch (Exception uaException)
      {
          errors.Add($"UA Error: {uaException.Message}");
      }

      if (errors.Count != 0)
      {
        return opcServers.Count != 0
          ? opcServers.ToArray()
          : throw new Exception($"Errors occurred: {string.Join("; ", errors)}");
      }

      return opcServers.ToArray();
    }

    public override bool ServerExists(string url, string? host = null)
    {
      OpcServer[] servers = BrowseServers(true, host);

      return servers.Any(server => server.Url == url);
    }
  }
}
