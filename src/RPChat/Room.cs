using RPChat.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPChat
{
    /// <summary>
    /// Represents an arbitrary conversation.
    /// </summary>
    public abstract class Room
    {
        /// <summary>
        /// Gets which characters are in the room.
        /// </summary>
        public HashSet<Character> Characters { get; } = new HashSet<Character>();

        /// <summary>
        /// Gets or sets the description of the room.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the room.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the short name of the room (for URLs and such).
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Gets which users are in the room.
        /// </summary>
        public HashSet<User> Users { get; } = new HashSet<User>();

        public abstract Task Leave(User user);

        public abstract Task SendMessage(Character character, string message);

        //public IEnumerable<string> Messages { get; set; }
    }
}