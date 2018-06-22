using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class ServiceUpgrades
    {
        [Key]
        public int UpgradeId { get; set; }
        [Required]
        public string UpgradeName { get; set; }
        [Required]
        public string UpgradeDescription { get; set; }
        [Required]
        public int Time { get; set; }
        [Required]
        public Decimal Price { get; set; }
    }
}