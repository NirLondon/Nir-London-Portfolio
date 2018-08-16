using ASP_Project.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Project.DAL
{
    public class DataCollector
    {
        //static ASPProjectDB DB = new ASPProjectDB();

        public static void AddUser(User user)
        {
            using (var DB = new ASPProjectDB())
            {
                DB.Users.Add(user);
                DB.SaveChanges();
            }
        }

        public static void AddUser(SigninUser user)
        {
            AddUser(new User
            {
                BirthDate = user.BirthDate,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                UserName = user.Username,
            });
        }

        public static void UploadProduct(UploadedProduct product, string userId)
        {
            using (var DB = new ASPProjectDB())
            {
                byte[][] imgsbfr = new byte[3][];
                for (int i = 0; i < product.Images.Length; i++)
                {
                    int? length = product.Images[i]?.ContentLength;
                    imgsbfr[i] = length.HasValue ? new byte[length.Value] : null;
                    product.Images[i]?.InputStream.Read(imgsbfr[i], 0, imgsbfr[i].Length);
                }

                //var owner = DB.Users.Find(int.Parse(userId));
                Product p = new Product
                {
                    UploadingDate = DateTime.Now,
                    Image1 = imgsbfr[0],
                    Image2 = imgsbfr[1],
                    Image3 = imgsbfr[2],
                    LongDescription = product.LongDescription,
                    Price = product.Price,
                    State = ProductState.Available,
                    ShortDescription = product.ShortDescription,
                    Title = product.Title,
                    OwnerId = int.Parse(userId),
                };

                //owner.OwnedProducts.Add(p);
                DB.Products.Add(p);
                DB.SaveChanges();
            }
        }

        public static void UpdateUser(SigninUser user)
        {
            using (var DB = new ASPProjectDB())
            {
                var updated = DB.Users.First(u => u.UserName == user.Username);
                updated.FirstName = user.FirstName;
                updated.LastName = user.LastName;
                updated.Password = user.Password;
                updated.BirthDate = user.BirthDate;
                DB.SaveChanges();
            }
        }

        public static void RemoveProduct(int productId)
        {
            using (var DB = new ASPProjectDB())
            {
                DB.Products.Remove(DB.Products.Find(productId));
                DB.SaveChanges();
            }
        }

        public static void BuyProduct(int productId/*, int userId*/)
        {
            using (var DB = new ASPProjectDB())
            {
                var prod = DB.Products.Find(productId);
                prod.State = ProductState.sold;
                DB.SaveChanges();
            }
        }

        public static void SaveProductFor(string productId, string userId = null)
        {
            using (var DB = new ASPProjectDB())
            {
                var prod = DB.Products.Find(int.Parse(productId));
                User user = null;
                if (!string.IsNullOrEmpty(userId))
                {
                    user = DB.Users.Find(int.Parse(userId));
                    prod.State = ProductState.Occupied;
                }
                else prod.ReleaseTime = DateTime.Now + new TimeSpan(0, 30, 0);

                user?.ProductsInCart.Add(prod);
                DB.SaveChanges();
            }
        }

        public static void ReleaseProducts(params string[] ids)
        {
            using (var DB = new ASPProjectDB())
            {
                var prods = DB.Products.Where(p => ids.Contains(p.Id.ToString()));
                foreach (var prod in prods)
                {
                    prod.State = ProductState.Available;
                    prod.ReleaseTime = DateTime.Now;
                    if (prod.UserId.HasValue)
                    {
                        var user = DB.Users.First(u => u.ProductsInCart.Select(p => p.Id).Contains(prod.Id));
                        user.ProductsInCart.Remove(user.ProductsInCart.First(p => p.Id == prod.Id));
                    }
                }
                DB.SaveChanges();
            }
        }
    }
}
