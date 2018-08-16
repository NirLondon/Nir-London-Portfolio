using Backgammon.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.DAL
{
    public class DBManager
    {
        //private DBManager() { }
        //static DBManager instance;
        //public static DBManager Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //            instance = new DBManager();
        //        return instance;
        //    }
        //}

        string connectionString;

        public User GetUser(string userName, string password)
        {
            using (var db = new BackgammonContext(/*connectionString*/))
            {
                var user = db.Users.Find(userName);
                return user?.Password == password ? user : null;
            }
        }

        public User GetUser(string userName)
        {
            using (var db = new BackgammonContext(/*connectionStrin*/))
                return db.Users.Find(userName);
        }

        public bool AddUser(string userName, string password)
        {
            using(var db = new BackgammonContext(/*connectionString*/))
            {
                if (db.Users.Any(u => u.UserName == userName))
                    return false;
                db.Users.Add(new User
                {
                    UserName = userName,
                    Password = password
                });
                db.SaveChanges();
                return true;
            }
        }
    }
}
