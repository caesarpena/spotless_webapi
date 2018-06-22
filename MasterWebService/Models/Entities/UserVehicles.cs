using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class UserVehicles
    {
        [Key]
        public int VehicleId { get; set; }
        [Required]
        public string MakeName { get; set; }
        [Required]
        public string ModelName { get; set; }
        [Required]
        public string Trim { get; set; }
        [Required]
        public string Year { get; set; }

        public string UserId { get; set; }

        public bool? Deleted { get; set; }
    }

    public class TrimFees
    {
        [Key]
        public int FeeId { get; set; }
        [Required]
        public string Trim { get; set; }
        [Required]
        public decimal Fees { get; set; }
    }
}