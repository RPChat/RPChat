using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPChat.Server
{
    public class GlobalChatRoom
    {
        private HashSet<IChatRoomListener> listeners { get; } = new HashSet<IChatRoomListener>();

        public IChatRoomMembership Join(IChatRoomListener listener)
        {
            listeners.Add(listener);
            return new GlobalChatRoomMembership(listeners, listener);
        }
    }
}
