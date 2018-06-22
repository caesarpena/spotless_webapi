using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class CustomerAddress
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string CityArea { get; set; }
        [Required]
        public string AddressDesc { get; set; }

        public string UserId { get; set; }

        public bool? Deleted { get; set; }

    }
}