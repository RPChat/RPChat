using Microsoft.AspNetCore.Http;
using RPChat.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPChat.Web
{
    public class WebServer : Server
    {
        private readonly HashSet<User> users = new HashSet<User>();
        public string Name { get; }

        public WebServer(string name)
        {
            Name = name;
        }

        public async Task AcceptConnection(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var user = new WebUser(socket);
            users.Add(user);

            user.Disconnected += usr => users.Remove(usr);
            user.ReceivedMessage += user_ReceivedMessage;

            await user.SendMessage(new Character() { Name = "Server" }, "Welcome!");

            await user.Receive();
        }

        private async void user_ReceivedMessage(User sender, string message)
        {
            // Parse Message n stuff

            foreach (var user in users.Except(new[] { sender }))
                await user.SendMessage(sender.MetaCharacter, message);
        }
    }
}