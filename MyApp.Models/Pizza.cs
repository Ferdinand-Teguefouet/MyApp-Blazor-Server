using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string[] Ingredients { get; set; }

        [Required]
        [Range(minimum: 1, maximum: 99)]
        public int Price { get; set; }

        [Required]
        public string ImageName { get; set; }


    }
}
