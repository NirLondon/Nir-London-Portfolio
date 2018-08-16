using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.ComponentModel;

namespace ASP_Project.Common.Models
{
    public class UploadedProduct
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [DisplayName("Title:")]
        public string Title { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2)]
        [DisplayName("Short Description:")]
        public string ShortDescription { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(4000, MinimumLength = 50)]
        [DisplayName("Long Description:")]
        public string LongDescription { get; set; }

        [Required]
        [Range(2, 18000)]
        [DataType(DataType.Currency)]
        [DisplayName("Price:")]
        public double Price { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [DisplayName("Add Picture(s)")]
        public HttpPostedFileBase[] Images { get; set; }

        //public byte[][] Images { get; set; }
    }
}
