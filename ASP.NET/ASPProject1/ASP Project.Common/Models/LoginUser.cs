using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP_Project.Common.Models
{
    public class LoginUser
    {
        [ScaffoldColumn(false)]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Enter your username.")]
        [DisplayName("Username:")]
        public string Username { get; set; }

        [ScaffoldColumn(false)]
        public string FirstName { get; set; }

        [ScaffoldColumn(false)]
        public string LastNmae { get; set; }

        [Required(ErrorMessage = "Enter your password.")]
        [DisplayName("Password:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}