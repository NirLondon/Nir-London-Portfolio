using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_Project.DAL
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public int OwnerId { get; set; }
        //[Required, ForeignKey("OwnerId")]
        public virtual User Owner { get; set; }

        public int? UserId { get; set; }
        //[ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required, StringLength(50)]
        public string Title { get; set; }

        [Required, StringLength(500, MinimumLength = 2)]
        public string ShortDescription { get; set; }

        [Required, StringLength(4000, MinimumLength = 50)]
        public string LongDescription { get; set; }

        [Required, DataType(DataType.Date)]
        [Column(name: "Uploading Date")]
        public DateTime UploadingDate { get; set; }

        [Required]
        //[Range(2, 1800)]
        //[DataType(DataType.Currency)]
        public double Price { get; set; }

        public byte[] Image1 { get; set; }

        public byte[] Image2 { get; set; }

        public byte[] Image3 { get; set; }

        [Required]
        public ProductState State { get; set; }

        [DataType(DataType.DateTime), Column(name: "Release Time")]
        public DateTime? ReleaseTime { get; set; }
    }

    public enum ProductState
    {
        Available = 0,
        Occupied = 1,
        sold = 2
    }
}
