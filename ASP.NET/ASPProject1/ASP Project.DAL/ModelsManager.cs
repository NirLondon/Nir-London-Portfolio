using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASP_Project.Common.Models;
using ASP_Project.DAL;

namespace ASP_Project.DAL
{
    public static class ModelsManager
    {
        public static IEnumerable<ClientProduct> ToClientProducts(this IEnumerable<Product> products)
        {
            return products.Select(p => p.ToClientProduct()).ToList();
        }

        public static ClientProduct ToClientProduct(this Product dbProd)
        {
            return new ClientProduct(dbProd.Id, dbProd.Title, dbProd.ShortDescription, dbProd.UploadingDate, dbProd.LongDescription, dbProd.Price, dbProd.OwnerId, dbProd.Image1, dbProd.Image2, dbProd.Image3);
        }


        public static LoginUser ToLoginUser(this User user)
        {
            return new LoginUser
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastNmae = user.LastName,
                Password = user.Password,
                Username = user.UserName,
            };
        }
    }
}
