using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_Project.DAL
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        //[Index("Email Adress", IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Index("UserName", IsUnique = true)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[ForeignKey("OwnerId")]
        public virtual ICollection<Product> OwnedProducts { get; set; }

        //[ForeignKey("UserId")]
        public virtual ICollection<Product> ProductsInCart { get; set; }

        public User()
        {
            OwnedProducts = new List<Product>();
            ProductsInCart = new List<Product>();
        }

        //public User(string firstname, string lastname, DateTime birthDate, string email, string username, string password)
        //{
        //    FirstName = firstname;
        //    LastName = lastname;
        //    BirthDate = birthDate;
        //    Email = email;
        //    UserName = username;
        //    Password = password;
        //}
    }
}
