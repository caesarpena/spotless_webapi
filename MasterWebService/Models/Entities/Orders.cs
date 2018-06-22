using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public DateTime ActualDate { get; set; }
        [Required]
        public DateTime ServiceDate { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public string Status { get; set; }

        public int AddressId { get; set; }

        public string UserId { get; set; }

        public string PaymentMethod { get; set; }

        public string StripeCreditCardId { get; set; }

        public string last4 { get; set; }

    }
    public class OrdersDetails

    {
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressDesc { get; set; }
        public string City { get; set; }
        public string CityArea { get; set; }
        public string ZipCode { get; set; }
        public DateTime ServiceDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public string StripeCreditCardId { get; set; }
        public string last4 { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public string ModelName { get; set; }
        public string Year { get; set; }

    }
}