using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class VehicleModel
    {
        [Key]
        public int ModelId { get; set; }

        public string ModelName { get; set; }
 
        public string Trim { get; set; }

        //public int MakeId { get; set; }

    }
}