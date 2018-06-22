using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class VehicleMake
    {
        [Key]
        public int MakeId { get; set; }
        [Required]
        public string MakeName { get; set; }
    }
}