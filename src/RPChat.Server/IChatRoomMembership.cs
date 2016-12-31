using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPChat.Server
{
    public interface IChatRoomMembership
    {
        Task Post(string message);
        void Leave();
    }
}
