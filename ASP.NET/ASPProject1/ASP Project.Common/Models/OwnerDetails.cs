using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Project.Common.Models
{
    public class OwnerDetails
    {
        [DisplayName("First name:")]
        public string FirstName { get; set; }
        [DisplayName("Last Name:")]
        public string LastName { get; set; }
        [DisplayName("Email Adress:")]
        public string Email { get; set; }
    }
}
