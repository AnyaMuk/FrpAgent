using System.Text;
using System.Net.WebSockets;
using System.Text.Json;

namespace FrpAgent
{
    class Connector
    {
        public Uri _uri { get; set; }
        ClientWebSocket ws_client;
        public async Task ConnectWS()
        {
            ws_client = new ClientWebSocket();
            await ws_client.ConnectAsync(_uri,CancellationToken.None);
            if(ws_client.State != WebSocketState.Open)
            {
                Console.WriteLine("连接失败");
            }
            else
            {
                Console.WriteLine("连接成功");
                var receiveTask = Task.Run(() => ReceiveMessage()); //移交后台任务
                var healthcheckerTask = Task.Run(() => HealthChecker());
            }
        }

        async Task ReceiveMessage()
        {
            var buffer = new byte[4096];
            while (ws_client.State == WebSocketState.Open)
            {
                var received = await ws_client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                var message = Encoding.UTF8.GetString(buffer, 0, received.Count);
                var JsonSerializeOption = new JsonSerializerOptions{ WriteIndented = true};
                var json = JsonSerializer.Serialize(message,JsonSerializeOption);
                Console.WriteLine($"{message}");
            }
        }

        async Task HealthChecker()
        {
            while(true)
            {
                if (ws_client.CloseStatus.HasValue)
                {
                    await ws_client.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                    Console.WriteLine("断开连接");
                    break;
                }
            }
        }
    }
}
