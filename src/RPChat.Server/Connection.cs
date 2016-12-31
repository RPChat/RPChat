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
    public class Connection
    {
        private WebSocket socket;

        public Connection(WebSocket socket)
        {
            this.socket = socket;
        }
        
        public async Task Serve()
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
