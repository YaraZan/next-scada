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

    public override OpcDa[] BrowseLocalDaServers()
    {
      var serverElementCollection = daClient.BrowseServers("");
      var servers = serverElementCollection
        .Select(server => new OpcDa(
          server.Description,
          server.ProgId,
          server.UrlString))
        .ToArray();

      return servers;
    }

    public override OpcUa[] BrowseLocalUaServers()
    {
      var serverElementCollection = uaClient.DiscoverLocalServers("opcua.demo-this.com");
      var servers = serverElementCollection
        .Select(server => new OpcUa(
          server.ApplicationName,
          server.DiscoveryUriString,
          server.ApplicationUriString))
        .ToArray();

      return servers;
    }

    public override OpcDa[] BrowseRemoteDaServers(string host)
    {
      var serverElementCollection = daClient.BrowseServers(host);
      var servers = serverElementCollection
        .Select(server => new OpcDa(
          server.Description,
          server.ProgId,
          server.UrlString))
        .ToArray();

      return servers;
    }

    public override OpcUa[] BrowseRemoteUaServers(string host)
    {
      var serverElementCollection = uaClient.DiscoverLocalServers(host);
      var servers = serverElementCollection
        .Select(server => new OpcUa(
          server.ApplicationName,
          server.DiscoveryUriString,
          server.ApplicationUriString))
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
          var daServers = BrowseRemoteDaServers(host ?? "");
          opcServers.AddRange(daServers.Cast<OpcServer>());
        }
        catch (Exception daException)
        {
          errors.Add($"DA Error: {daException.Message}");
        }
      }

      try
      {
          var uaServers = BrowseRemoteUaServers(host ?? "opcua.demo-this.com");
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
  }
}
