using Backgammon.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.DAL
{
    public class BackgammonContext : DbContext
    {
        public BackgammonContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new BackgammonContextInitializer());
        }

        public BackgammonContext() : base("BackgammonDB") { }

        public virtual DbSet<User> Users { get; set; }
    }
}
