using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP_Project.Common.Models;

namespace ASP_Project.DAL
{
    public static class DataAccessor
    {
        public static IEnumerable<Product> GetAllProducts(string userId = null)
        {
            using (var DB = new ASPProjectDB())
            {
                if (int.TryParse(userId, out int id))
                    return DB.Products.Where(p => p.State == ProductState.Available &&
                    p.OwnerId != id &&
                    (p.ReleaseTime < DateTime.Now || p.ReleaseTime == null)
                    ).ToList();
                return DB.Products.Where(p => p.State == ProductState.Available &&
                    (p.ReleaseTime < DateTime.Now || p.ReleaseTime == null)
                    ).ToList();
            }
        }

        public static IEnumerable<Product> GetProductsByIds(params int[] ids)
        {
            if (ids == null) return null;
            using (var DB = new ASPProjectDB())
                return DB.Products.Where(p => ids.Contains(p.Id)).ToList();
        }

        public static bool Exists(LoginUser user)
        {
            using (var DB = new ASPProjectDB())
                if (DB.Users.Any(u => u.UserName == user.Username && u.Password == user.Password))
                {
                    var u1 = DB.Users.First(u => u.UserName == user.Username);
                    //loginUser = new LoginUser
                    //{
                    //    Id = u1.Id,
                    //    Username = u1.UserName,
                    //    FirstName = u1.FirstName,
                    //    LastNmae = u1.LastName,
                    //};

                    user.Id = u1.Id;
                    user.Username = u1.UserName;
                    user.FirstName = u1.FirstName;
                    user.LastNmae = u1.LastName;
                    return true;
                }
                else { /*loginUser = null;*/ return false; }
        }

        public static bool Exists(string key, ExistenceCheckOption checkBy)
        {
            using (var DB = new ASPProjectDB())
            {
                switch (checkBy)
                {
                    case ExistenceCheckOption.ByUsername:
                        return DB.Users.Any(u => u.UserName == key);
                    case ExistenceCheckOption.ByEmail:
                        return DB.Users.Any(u => u.Email == key);
                    case ExistenceCheckOption.ById:
                        return DB.Users.Any(u => u.Id.ToString() == key);
                    default: return false;
                }
            }
        }

        public static LoginUser GetUser(string username, string password)
        {
            using (var DB = new ASPProjectDB())
                return DB.Users.First(u => u.UserName == username && u.Password == password).ToLoginUser();
        }

        public static SigninUser GetUser(int userId)
        {
            using (var DB = new ASPProjectDB())
            {
                var user = DB.Users.Find(userId);
                return new SigninUser
                {
                    Username = user.UserName,
                    Email = user.Email,
                    BirthDate = user.BirthDate,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
            }
        }

        public static OwnerDetails GetOwnerDetails(int ownerId)
        {
            using (var DB = new ASPProjectDB())
            {
                var owner = DB.Users.Find(ownerId);
                return new OwnerDetails { FirstName = owner.FirstName, LastName = owner.LastName, Email = owner.Email };
            }
        }

        public static ClientProduct GetProduct(int productId)
        {
            using (var DB = new ASPProjectDB())
            {
                return DB.Products.Find(productId).ToClientProduct();
            }
        }

        public static IEnumerable<ClientProduct> GetCartOf(string username, string userId = null)
        {
            using (var DB = new ASPProjectDB())
            {
                if (userId != null)
                    return DB.Users.Find(int.Parse(userId)).ProductsInCart.Where(p => p.State == ProductState.Occupied).ToClientProducts();

                return DB.Users.First(u => u.UserName == username).ProductsInCart.Where(p => p.State == ProductState.Occupied).ToClientProducts();
            }
        }

        public static IEnumerable<ClientProduct> GetProductsOf(string username, string userId = null)
        {
            using (var DB = new ASPProjectDB())
            {
                if (userId != null) return DB.Users.Find(int.Parse(userId)).OwnedProducts.ToClientProducts();
                return DB.Users.First(u => u.UserName == username).OwnedProducts.ToClientProducts();
            }
        }

        public static int[] GetProductsIds(int userId)
        {
            using (var DB = new ASPProjectDB())
            {
                return DB.Users.Find(userId).ProductsInCart.Select(p => p.Id).ToArray();
            }
        }
    }

    public enum ExistenceCheckOption
    {
        ByUsername = 0,
        ByEmail = 1,
        ById = 2,
    }
}
