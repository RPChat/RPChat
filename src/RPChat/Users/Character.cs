using System;
using System.Collections.Generic;
using System.Linq;

namespace RPChat.Users
{
    /// <summary>
    /// Represents a Character usable in chat.
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Gets or sets the description of the character.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the images associated with the character.
        /// </summary>
        public HashSet<Image> Images { get; private set; }

        /// <summary>
        /// Gets or sets the name of the character.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the Users that are allowed to play the character or played it before.
        /// </summary>
        public Dictionary<User, Usage> Users { get; private set; }

        /// <summary>
        /// Flags for the possible usage-status of a <see cref="User"/> concerning a <see cref="Character"/>.
        /// <para/>
        /// Allowed not set means forbidden to use and Played not set means never played it.
        /// </summary>
        [Flags]
        public enum Usage
        {
            /// <summary>
            /// User is not allowed to play the character and never did.
            /// </summary>
            None = 0,

            /// <summary>
            /// User is allowed to play the character.
            /// </summary>
            Allowed = 1,

            /// <summary>
            /// User played the character before.
            /// </summary>
            Played = 1 << 1,

            /// <summary>
            /// User is allowed to play the character and did so before.
            /// </summary>
            AllowedAndPlayed = Allowed | Played
        }
    }
}