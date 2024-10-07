using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using api.Core;
using api.Core.api.Core;
using api.Exceptions;
using api.Models.Opc;
using api.Models.OpcItem;
using api.Models.OpcServerNode;
using api.Requests;
using api.Services.Meta;
using OpcLabs.EasyOpc;
using OpcLabs.EasyOpc.DataAccess;
using OpcLabs.EasyOpc.DataAccess.OperationModel;
using OpcLabs.EasyOpc.UA;
using OpcLabs.EasyOpc.UA.OperationModel;

namespace api.Services.Impl
{
  public class OpcService : OpcServiceMeta
  {
    private static readonly EasyDAClient daClient = new();

    private static readonly EasyUAClient uaClient = new();

    private static readonly TimeSpan timeout = TimeSpan.FromSeconds(5);

    private static readonly int updateRate = 1000;

    public override OpcDa[] BrowseDaServers(string? host = null)
    {
      var serverElementCollection = daClient.BrowseServers(host ?? "");
      var servers = serverElementCollection
        .Select(server => new OpcDa(
          server.Description,
          server.UrlString,
          server.ProgId,
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
          server.ApplicationUriString,
          server.DiscoveryUriString,
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
          var daServers = OpcOperationTimeoutHandler(() => BrowseDaServers(host), timeout);
          opcServers.AddRange(daServers.Cast<OpcServer>());
        }
        catch (Exception daException)
        {
          errors.Add($"DA Error: {daException.Message}");
        }
      }

      try
      {
        var uaServers = OpcOperationTimeoutHandler(() => BrowseUaServers(host), timeout);
        opcServers.AddRange(uaServers.Cast<OpcServer>());
      }
      catch (Exception uaException)
      {
        errors.Add($"UA Error: {uaException.Message}");
      }

      if (opcServers.Count == 0 && errors.Count > 0)
      {
        throw new OpcBrowsingException(string.Join("; ", errors));
      }

      return [.. opcServers];
    }

    public override OpcServerNode[] BrowseServerNodes(BrowseServerNodesRequest request)
    {
      try
      {
        if (request.Protocol == "DA")
        {
          return BrowseDaNodes(request.ConnectionString, request.NodeId, request.Host);
        }
        else if (request.Protocol == "UA")
        {
          return BrowseUaNodes(request.ConnectionString, request.NodeId);
        }
        else
        {
          throw new Exception();
        }
      }
      catch (Exception)
      {
        throw new OpcItemsBrowsingException();
      }
    }

    private static OpcServerNode[] BrowseDaNodes(string connectionString, string? nodeId = null, string? host = null)
    {
      List<OpcServerNode> opcItems = [];

      var branches = daClient.BrowseBranches(host ?? "", connectionString, nodeId ?? "");
      foreach (var branch in branches)
      {
        opcItems.Add(new OpcFolder(
            branch.ItemId,
            branch.Name
        ));
      }

      var leaves = daClient.BrowseLeaves(host ?? "", connectionString, nodeId ?? "");
      foreach (var leaf in leaves)
      {
        opcItems.Add(new OpcTag(
            leaf.ItemId,
            leaf.Name
        ));
      }

      return [.. opcItems];
    }

    private static OpcServerNode[] BrowseUaNodes(string connectionString, string? nodeId = null)
    {
      List<OpcServerNode> opcItems = [];

      var nodes = nodeId == null
          ? uaClient.BrowseDataNodes(connectionString)
          : uaClient.BrowseDataNodes(connectionString, nodeId);

      foreach (var node in nodes)
      {
        var childNodes = uaClient.BrowseDataNodes(connectionString, node.NodeId);

        if (childNodes.Any())
        {
          opcItems.Add(new OpcFolder(
              node.NodeId,
              node.DisplayName
          ));
        }
        else
        {
          opcItems.Add(new OpcTag(
              node.NodeId,
              node.DisplayName
          ));
        }
      }

      return [.. opcItems];
    }

    public override bool ServerExists(string url, string? host = null)
    {
      try
      {
        OpcServer[] servers = BrowseServers(true, host);

        return servers.Any(server => server.ConnectionString == url);
      }
      catch (Exception)
      {
        throw new OpcServerExistsCheckException();
      }
    }

    public override int Subscribe(SubscriptionRequest request)
    {
      try
      {
        if (request.Protocol == "DA")
        {
          daClient.ItemChanged += DaTagChanged;

          return daClient.SubscribeItem(request.Host ?? "", request.ConnectionString, request.TagId, updateRate);
        }
        else if (request.Protocol == "UA")
        {
          uaClient.DataChangeNotification += UaTagChanged;

          return uaClient.SubscribeDataChange(request.ConnectionString, request.TagId, updateRate);
        }
        else
        {
          throw new Exception();
        }

      }
      catch (Exception)
      {
        throw new OpcItemSubscriptionException();
      }
    }

    public override void Unsubscribe(UnsubscriptionRequest request)
    {
      try
      {
        if (request.Protocol == "DA")
        {
          daClient.UnsubscribeItem(request.SubscribedTagId);
        }
        else if (request.Protocol == "UA")
        {
          uaClient.UnsubscribeMonitoredItem(request.SubscribedTagId);
        }
      }
      catch (Exception)
      {
        throw new OpcItemUnsubscriptionException();
      }
    }

    private static async void DaTagChanged(object sender, EasyDAItemChangedEventArgs e)
    {
      string tagId = e.Arguments.ItemDescriptor.ItemId;

      if (e.Succeeded)
      {
        var opcTagChanged = new OpcTagChanged(
          tagId,
          e.Vtq.Value,
          e.Vtq.Quality,
          e.Vtq.Timestamp
        );

        await WebSocketHandler.BroadcastMessageAsync(JsonSerializer.Serialize(opcTagChanged));
      }
      else
      {
        var opcTagErrored = new OpcTagErrored(
          tagId,
          e.ErrorMessage
        );

        await WebSocketHandler.BroadcastMessageAsync(JsonSerializer.Serialize(opcTagErrored));
      }
    }

    private static async void UaTagChanged(object sender, EasyUADataChangeNotificationEventArgs e)
    {
      string tagId = e.Arguments.NodeDescriptor.NodeId;
      var newValue = e.AttributeData.Value;

      if (e.Succeeded)
      {
        var opcTagChanged = new OpcTagChanged(
          tagId,
          newValue,
          e.AttributeData.ServerTimestamp,
          e.AttributeData.StatusCode
        );

        await WebSocketHandler.BroadcastMessageAsync(JsonSerializer.Serialize(opcTagChanged));
      }
      else
      {
        var opcTagErrored = new OpcTagErrored(
          tagId,
          e.ErrorMessage
        );

        await WebSocketHandler.BroadcastMessageAsync(JsonSerializer.Serialize(opcTagErrored));
      }
    }

    private static T OpcOperationTimeoutHandler<T>(Func<T> fn, TimeSpan timeout)
    {
      Task<T> browseTask = Task.Run(fn);
      if (Task.WhenAny(browseTask, Task.Delay(timeout)).Result == browseTask)
      {
        return browseTask.Result;
      }
      else
      {
        throw new TimeoutException("The operation timed out while browsing OPC servers.");
      }
    }
  }
}
