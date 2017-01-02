using RPChat.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPChat.Web
{
    public class WebRoom : Room
    {
        public WebRoom()
        {
            Name = "Global";
            ShortName = "global";
            Description = "Global Room for All!";
        }

        public override async Task Leave(User user)
        {
            Users.Remove(user);
        }

        public override async Task SendMessage(Character character, string message)
        {
            foreach (var user in Users)
                await user.SendMessage(character, message);
        }
    }
}