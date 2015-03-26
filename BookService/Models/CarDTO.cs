using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarService.Models
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OwnerName { get; set; }
    }
}