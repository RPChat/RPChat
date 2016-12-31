using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPChat.Server
{
    public class Connection : IChatRoomListener
    {
        private readonly WebSocket socket;
        private readonly IChatRoomMembership membership;

        public Connection(WebSocket socket, Func<IChatRoomListener,IChatRoomMembership> join)
        {
            this.socket = socket;
            this.membership = join(this);
        }

        public async Task OnMessage(string message)
        {
            var outgoing = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
            await socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        
        public async Task Serve()
        {
            var buffer = new byte[4096];
            var segment = new ArraySegment<byte>(buffer);
            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    var incoming = await socket.ReceiveAsync(segment, CancellationToken.None);
                    var message = Encoding.UTF8.GetString(buffer, 0, incoming.Count);
                    await membership.Post(message);
                }
            }
            finally
            {
                membership.Leave();
            }
        }
    }
}
