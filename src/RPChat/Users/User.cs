using System;
using System.Collections.Generic;
using System.Linq;

namespace RPChat.Users
{
    /// <summary>
    /// Represents a user of the chat. One user per connection.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets the Characters a user has access to.
        /// </summary>
        public HashSet<Character> Characters { get; private set; }

        /// <summary>
        /// Gets or sets the identity of the user.
        /// </summary>
        public Identity Identity { get; set; }
    }
}