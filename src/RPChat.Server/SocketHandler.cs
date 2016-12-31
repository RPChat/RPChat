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
        private WebSocket socket;

        private SocketHandler(WebSocket socket)
        {
            this.socket = socket;
        }
        
        public static async Task Accept(HttpContext context, Func<Task> n)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                return;
            }
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var handler = new SocketHandler(socket);
            await handler.EchoLoop();
        }

        private async Task EchoLoop()
        {
            var buffer = new byte[4096];
            var segment = new ArraySegment<byte>(buffer);
            while (socket.State == WebSocketState.Open)
            {
                var incoming = await socket.ReceiveAsync(segment, CancellationToken.None);
                var outgoing = new ArraySegment<byte>(buffer, 0, incoming.Count);
                await socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}