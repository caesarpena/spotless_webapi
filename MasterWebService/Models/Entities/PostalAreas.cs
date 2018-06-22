using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class PostalAreas
    {
        [Key]
        public string PostalCode { get; set; }
    }
}