﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterWebService.Models.Entities
{
    public class _Models
    {
        [Required]
        public string ModelName { get; set; }
    }
}