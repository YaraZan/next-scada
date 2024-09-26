
using System.Net.WebSockets;
using System.Text;

namespace api.Core
{
  using System.Net.WebSockets;
  using System.Text;
  using System.Collections.Concurrent;

  namespace api.Core
  {
    public static class WebSocketHandler
    {
      // Use a concurrent dictionary to manage multiple WebSocket clients
      private static readonly ConcurrentDictionary<string, WebSocket> Clients = new ConcurrentDictionary<string, WebSocket>();

      public static async Task HandleWebSocketAsync(WebSocket webSocket)
      {
        string clientId = Guid.NewGuid().ToString();
        Clients.TryAdd(clientId, webSocket);

        var buffer = new byte[1024 * 4];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!result.CloseStatus.HasValue)
        {
          result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        Clients.TryRemove(clientId, out _);

        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
      }

      // Broadcast to all connected WebSocket clients
      public static async Task BroadcastMessageAsync(string message)
      {
        var messageBytes = Encoding.UTF8.GetBytes(message);

        foreach (var client in Clients.Values)
        {
          if (client.State == WebSocketState.Open)
          {
            await client.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
          }
        }
      }
    }
  }
}
