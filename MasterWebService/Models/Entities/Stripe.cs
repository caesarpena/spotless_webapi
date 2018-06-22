using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class Stripe
    {
    }

    public class CreateStripeCustomer
    {
        public string AddressState { get; set; }
        public string AddressCity { get; set; }
        public string AddressZip { get; set; }
        public string AddressLine1 { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVC { get; set; }
    }

    /* public class CreateStripeCustomer
     {
         public string token { get; set; }
     }*/

    public class StripeChargeOrder
    {
        public int amount { get; set; }
        public string card { get; set; }
        public string orderid { get; set; }
        public string description { get; set; }
        public string status { get; set; }
    }

}