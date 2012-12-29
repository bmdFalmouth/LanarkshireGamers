using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LanarkshireGamersData.Model;

namespace LanarkshireGamersData
{
    public class LanarkshireGamersContext:DbContext
    {
        public LanarkshireGamersContext()
        {

#if DEBUG
            Database.SetInitializer(new DropCreateDatabaseAlways<LanarkshireGamersContext>());
#endif
        }

        public DbSet<User> User { get; set; }
        public DbSet<Game> Game { get; set; }
    }
}
