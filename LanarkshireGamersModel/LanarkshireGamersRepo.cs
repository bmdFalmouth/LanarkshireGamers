using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanarkshireGamersData.Model;
using System.Data;

namespace LanarkshireGamersData
{
    public class LanarkshireGamersRepo
    {
        private LanarkshireGamersContext context;

        static readonly LanarkshireGamersRepo instance = new LanarkshireGamersRepo();

        static LanarkshireGamersRepo()
        {

        }

        public static LanarkshireGamersRepo Instance
        {
            get
            {
                return instance;
            }
        }

        LanarkshireGamersRepo()
        {
            context = new LanarkshireGamersContext();
        }

        public User GetUserByUsername(string username)
        {
            User user = context.User.FirstOrDefault(u => u.UserName == username);
            return user;
        }

        public bool AddUser(User u)
        {
            context.User.Add(u);
            try
            {
                context.SaveChanges();
            }
            catch (InvalidOperationException ioe)
            {
                //rethrow as an add user exception
                return false;
            }
            return true;
        }

        public bool UpdateUser(User u)
        {
            context.Entry(u).State = EntityState.Modified;
            context.SaveChanges();
            return true;
        }


    }
}
