using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using api.Core;
using api.Core.api.Core;
using api.Exceptions;
using api.Models.Opc;
using api.Models.OpcItem;
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

    public override OpcItem[] BrowseServerItems(BrowseServerItemsRequest request)
    {
      try
      {
        if (request.IsDa)
        {
          return BrowseDaNodes(request.ConnectionString, "", request.Host);
        }
        else
        {
          return BrowseUaNodes(request.ConnectionString, null);
        }
      }
      catch (Exception)
      {
        throw new OpcItemsBrowsingException();
      }
    }

    private static OpcItem[] BrowseDaNodes(string connectionString, string branchId, string? host = null)
    {
      List<OpcItem> opcItems = [];

      var branches = daClient.BrowseBranches(host ?? "", connectionString, branchId);
      foreach (var branch in branches)
      {
        var childOpcItems = BrowseDaNodes(connectionString, branch.ItemId, host);

        opcItems.Add(new OpcItem(
            branch.ItemId,
            branch.Name,
            [.. childOpcItems]
        ));
      }

      var leaves = daClient.BrowseLeaves(host ?? "", connectionString, branchId);
      foreach (var leaf in leaves)
      {
        opcItems.Add(new OpcItem(
            leaf.ItemId,
            leaf.Name,
            null
        ));
      }

      return [.. opcItems];
    }
    private static OpcItem[] BrowseUaNodes(string connectionString, string? nodeId = null)
    {
      List<OpcItem> opcItems = [];

      var nodes = nodeId == null
          ? uaClient.BrowseDataNodes(connectionString)
          : uaClient.BrowseDataNodes(connectionString, nodeId);

      foreach (var node in nodes)
      {
        var childNodes = uaClient.BrowseDataNodes(connectionString, node.NodeId);

        if (childNodes.Any())
        {
          var childOpcItems = BrowseUaNodes(connectionString, node.NodeId);

          opcItems.Add(new OpcItem(
              node.NodeId,
              node.DisplayName,
              [.. childOpcItems]
          ));
        }
        else
        {
          opcItems.Add(new OpcItem(
              node.NodeId,
              node.DisplayName,
              null
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

    public override IEnumerable<int> Subscribe(SubscriptionRequest request)
    {
      List<int> subscibedIds = [];

      try
      {
        if (request.IsDa)
        {
          daClient.ItemChanged += DaItemChanged;

          foreach (var item in request.ItemPaths)
          {
            // Implement item existence check

            subscibedIds.Add(daClient.SubscribeItem(request.Host ?? "", request.ConnectionString, item, updateRate));
          }
        }
        else
        {
          uaClient.DataChangeNotification += UaItemChanged;

          foreach (var item in request.ItemPaths)
          {
            // Implement item existence check

            subscibedIds.Add(uaClient.SubscribeDataChange(request.ConnectionString, item, updateRate));
          }
        }
      }
      catch (Exception)
      {
        throw new OpcItemSubscriptionException();
      }

      return subscibedIds;
    }

    public override void Unsubscribe(UnsubscriptionRequest request)
    {
      try
      {
        if (request.IsDa)
        {
          foreach (var itemId in request.SubscribedItemIds)
          {
            daClient.UnsubscribeItem(itemId);
          }
        }
        else
        {
          foreach (var itemId in request.SubscribedItemIds)
          {
            uaClient.UnsubscribeMonitoredItem(itemId);
          }
        }
      }
      catch (Exception)
      {
        throw new OpcItemUnsubscriptionException();
      }
    }

    private static async void DaItemChanged(object sender, EasyDAItemChangedEventArgs e)
    {
      if (e.Succeeded)
      {
        string itemPath = e.Arguments.ItemDescriptor.ItemId;

        var opcItemChanged = new OpcItemChanged(
          itemPath,
          e.Vtq.Value,
          e.Vtq.Quality,
          e.Vtq.Timestamp
        );

        await WebSocketHandler.BroadcastMessageAsync(JsonSerializer.Serialize(opcItemChanged));
      }
      else
      {
        await WebSocketHandler.BroadcastMessageAsync($"Error: {e.ErrorMessage}");
      }
    }

    private static async void UaItemChanged(object sender, EasyUADataChangeNotificationEventArgs e)
    {
      string itemId = e.Arguments.NodeDescriptor.NodeId;
      var newValue = e.AttributeData.Value;

      if (e.Succeeded)
      {
        var opcItemChanged = new OpcItemChanged(
          itemId,
          newValue,
          e.AttributeData.ServerTimestamp,
          e.AttributeData.StatusCode
        );

        await WebSocketHandler.BroadcastMessageAsync(JsonSerializer.Serialize(opcItemChanged));
      }
      else
      {
        await WebSocketHandler.BroadcastMessageAsync($"Error: {e.ErrorMessage}");
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
