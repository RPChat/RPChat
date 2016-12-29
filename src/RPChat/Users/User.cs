using System;
using System.Collections.Generic;
using System.Linq;

namespace RPChat.Users
{
    public class User
    {
        public HashSet<Character> Characters { get; set; }
        public Identity Identity { get; set; }
    }
}