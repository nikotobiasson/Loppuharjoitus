using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models
{
    public class Owner
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}