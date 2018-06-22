using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class ProductService
    {
        [Key]
        public int ServiceId { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public string ServiceDescription { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public Decimal Price { get; set; }

    }


    public class ServiceInfo
    {
        [Key]
        public int ServiceId { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public string ServiceDescription { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public Decimal Price { get; set; }

        public Decimal Fee { get; set; }

        public string ImageUrl { get { return SetImage(Price); } }

        public Decimal FinalPrice { get { return SetFinalPrice(Price, Fee); } }

        Decimal SetFinalPrice(decimal price, decimal fee)
        {
            price = price + (price * fee);

            var charge = Math.Round(price, 2);

            return charge;
        }

        string SetImage(Decimal price)
        {
            var ImageUrl = "";

            if (price < (decimal)49.96)
            {
                ImageUrl = "happy2.png";
            }
            if (price > (decimal)30.99)
            {
                ImageUrl = "laughing.png";
            }
            if (price > (decimal)100.99)
            {
                ImageUrl = "cool.png";
            }
            if (price > (decimal)230.99)
            {
                ImageUrl = "inlove.png";
            }
            return ImageUrl;
        }
    }
}