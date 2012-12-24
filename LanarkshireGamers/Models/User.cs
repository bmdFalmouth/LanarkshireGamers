using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanarkshireGamers
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string GeekUserName { get; set; }
        public string FacebookUserName { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
