using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class OrderClientData
    {
        public int AddressId { get; set; }

        public string AddressDesc { get; set; }

        public string Status { get; set; }

        public decimal Total { get; set; }

        public DateTime ActualDate { get; set; }

        public DateTime ServiceDate { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string PaymentMethod { get; set; }

        public string StripeCreditCardId { get; set; }

        public string last4 { get; set; }
    }
}