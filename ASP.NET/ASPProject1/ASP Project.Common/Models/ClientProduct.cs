using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP_Project.Common.Models
{
    public class ClientProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public byte[][] Images { get; set; }

        public DateTime UploadingDate { get; set; }

        public int OwnerId { get; set; }

        public ClientProduct(int id, string title, string shortDescription, DateTime uploadingDate, string longDescription, double price, int ownerId, params byte[][] images)
        {
            Id = id;
            Title = title;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
            Price = price;
            UploadingDate = uploadingDate;
            OwnerId = ownerId;
            Images = images;
        }
        public ClientProduct()
        {

        }
    }
}