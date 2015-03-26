using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }

        // Foreign Key
        public int OwnerId { get; set; }
        // Navigation property
        public Owner Owner { get; set; }
    }
}