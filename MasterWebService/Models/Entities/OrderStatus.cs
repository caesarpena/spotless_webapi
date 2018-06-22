using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class OrderStatus
    {
        [Key]
        public int StatusId { get; set; }
        [Required]
        public string StatusDescription { get; set; }
    }
}