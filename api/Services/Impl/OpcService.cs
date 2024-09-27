using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using api.Core;
using api.Core.api.Core;
using api.Exceptions;
using api.Models.Opc;
using api.Models.Tag;
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
      try
      {
        List<int> subscibedIds = [];

        if (request.IsDa)
        {
          daClient.ItemChanged += DaItemChanged;

          foreach (var item in request.ItemPaths)
          {
            subscibedIds.Add(daClient.SubscribeItem(request.Host ?? "", request.ConnectionString, item, updateRate));
          }
        }
        else
        {
          uaClient.DataChangeNotification += UaItemChanged;

          foreach (var item in request.ItemPaths)
          {
            subscibedIds.Add(uaClient.SubscribeDataChange(request.ConnectionString, item, updateRate));
          }
        }
        return subscibedIds;
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

        var tag = new Tag(
          itemPath,
          e.Vtq.Value,
          e.Vtq.Quality,
          e.Vtq.Timestamp
        );

        await WebSocketHandler.BroadcastMessageAsync(JsonSerializer.Serialize(tag));
      }
      else
      {
        await WebSocketHandler.BroadcastMessageAsync($"Error: {e.ErrorMessage}");
      }
    }

    private static async void UaItemChanged(object sender, EasyUADataChangeNotificationEventArgs e)
    {
      if (e.Succeeded)
      {
        await WebSocketHandler.BroadcastMessageAsync($"Item updated: {e.AttributeData.Value}");
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
