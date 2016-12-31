using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPChat.Server
{
    public class GlobalChatRoomMembership : IChatRoomMembership
    {
        private HashSet<IChatRoomListener> listeners;
        private IChatRoomListener thisListener;

        public GlobalChatRoomMembership(HashSet<IChatRoomListener> listeners, IChatRoomListener thisListener)
        {
            this.listeners = listeners;
            this.thisListener = thisListener;
        }

        public void Leave()
        {
            listeners.Remove(thisListener);
        }

        public async Task Post(string message)
        {
            foreach (var listener in listeners)
            {
                await listener.OnMessage(message);
            }
        }
    }
}
