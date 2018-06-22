using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class OrderServiceClientData
    {
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public decimal ServicePrice { get; set; }

        public int VehicleId { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleTrim { get; set; }

        public string VehicleYear { get; set; }
    }
}