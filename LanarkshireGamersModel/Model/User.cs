using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LanarkshireGamersData.Model
{
    public class FacebookUser
    {
        public long id { get; set; } //yes. the user id is of type long...dont use int
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string email { get; set; }

    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        
        public string Password { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string EmailAddress { get; set; }
        public string GeekUserName { get; set; }
        public FacebookUser FacebookDetails { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
