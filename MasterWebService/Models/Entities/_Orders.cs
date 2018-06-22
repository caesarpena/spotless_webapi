using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class _Orders
    {
        public  OrderClientData orderdata { get; set; }
        public List<OrderServiceClientData> orderservicedata { get; set; }
       // public OrderUpgradesClientData orderupgradedata { get; set; }
    }
}