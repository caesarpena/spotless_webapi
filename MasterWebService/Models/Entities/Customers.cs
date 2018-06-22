using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class Customers
    {
    }

    public class CustomersDetails
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string StripeCustomerId { get; set; }
    }
}