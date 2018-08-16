using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Project.Common.Models
{
    public class OwnerProduct
    {
        public OwnerDetails Owner { get; set; }
        public ClientProduct Product { get; set; }
    }
}
