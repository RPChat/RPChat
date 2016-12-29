using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace RPChat.Server
{
    public class SocketHandler
    {
        public const int BufferSize = 4096;

        private WebSocket socket;

        private SocketHandler(WebSocket socket)
        {
            this.socket = socket;
        }

        public static void Map(IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(Acceptor);
        }

        private static async Task Acceptor(HttpContext hc, Func<Task> n)
        {
            if (!hc.WebSockets.IsWebSocketRequest)
                return;

            var socket = await hc.WebSockets.AcceptWebSocketAsync();
            var h = new SocketHandler(socket);
            await h.EchoLoop();
        }

        private async Task EchoLoop()
        {
            var buffer = new byte[BufferSize];
            var seg = new ArraySegment<byte>(buffer);

            while (socket.State == WebSocketState.Open)
            {
                var incoming = await socket.ReceiveAsync(seg, CancellationToken.None);
                var outgoing = new ArraySegment<byte>(buffer, 0, incoming.Count);
                await socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}