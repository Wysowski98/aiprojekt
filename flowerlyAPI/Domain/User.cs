using System;
using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public int Id { get; set; }
        public string UID { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public List<MyFlowers> Flowers { get; set; }
    }
}
