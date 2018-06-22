using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class OrderService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderId { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int VehicleId { get; set; }
        
    }
}