using RPChat.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPChat.Web
{
    public class WebUser : User
    {
        private readonly WebSocket socket;

        public WebUser(WebSocket socket)
        {
            this.socket = socket;
        }

        public async Task Receive()
        {
            var buffer = new byte[4096];
            var segment = new ArraySegment<byte>(buffer);
            try
            {
                while (socket.State == WebSocketState.Open)
                {
                    var incoming = await socket.ReceiveAsync(segment, CancellationToken.None);
                    var message = Encoding.UTF8.GetString(buffer, 0, incoming.Count);

                    ReceivedMessage?.Invoke(this, message);
                }
            }
            finally
            {
                Disconnected?.Invoke(this);
            }
        }

        public override async Task SendMessage(Character character, string message)
        {
            var outgoing = new ArraySegment<byte>(Encoding.UTF8.GetBytes($"{character.Name}: {message}"));
            await socket.SendAsync(outgoing, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public event Action<User> Disconnected;

        public event Action<User, string> ReceivedMessage;
    }
}