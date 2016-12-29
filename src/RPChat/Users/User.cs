using System;
using System.Collections.Generic;
using System.Linq;

namespace RPChat.Users
{
    public class User
    {
        public List<Character> Characters { get; set; }
        public Identity Identity { get; set; }
    }
}