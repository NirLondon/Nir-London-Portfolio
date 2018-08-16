using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;

namespace ASP_Project.DAL
{
    public class ASPProjectSeedInitializer : DropCreateDatabaseIfModelChanges<ASPProjectDB>
    {
        //protected override void Seed(ASPProjectDB context)
        //{
        //    var nir = new User
        //    {
        //        FirstName = "Nir",
        //        LastName = "London",
        //        Email = "nirlondon2009@gmail.com",
        //        BirthDate = new DateTime(1995, 5, 13),
        //        Password = "12345678",
        //        UserName = "NirLondon",
        //    };

        //    var shir = new User
        //    {
        //        FirstName = "Shir",
        //        LastName = "London",
        //        Email = "shir.london@gmail.com",
        //        BirthDate = new DateTime(2000, 4, 26),
        //        Password = "87654321",
        //        UserName = "ShirLondon",
        //    };

        //    var prod1 = new Product
        //    {
        //        Title = "IPhone",
        //        User = nir,
        //        ShortDescription = "IPhone 7",
        //        LongDescription = "IPhone 7 is great!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!",
        //        Price = 700,
        //        //Owner = shir,
        //        Image1 = File.ReadAllBytes(@"C:\Amir\iphone7.jpg"),
        //        Date = DateTime.Now
        //    };

        //    var prod2 = new Product
        //    {
        //        Title = "Galaxy",
        //        ShortDescription = "Galaxy S5",
        //        LongDescription = "Galaxy S5 is great!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!",
        //        Price = 700,
        //        //Owner = nir,
        //        Image1 = File.ReadAllBytes(@"C:\Amir\galaxyS5.jpg"),
        //        Date = DateTime.Now
        //    };

        //    shir.OwnedProducts.Add(prod1);
        //    nir.OwnedProducts.Add(prod2);

        //    context.Users.Add(nir);
        //    context.Users.Add(shir);

        //    context.SaveChanges();
        //}
    }
}
