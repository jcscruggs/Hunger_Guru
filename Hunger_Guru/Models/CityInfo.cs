using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hunger_Guru.Models
{
    public class CityInfo
    {
        [Required]
        [RegularExpression(@"^(([A-za-z]+)|([A-za-z]+[\s]{1}[A-za-z]+)|([A-za-z]+[\s]{1}[A-za-z]+[\s]{1}[A-za-z]+))$", ErrorMessage = "Please enter valid city name")]
        public String City { get; set; }

        [Required]
        [StringLength(2)]
        [RegularExpression(@"^[A-Z]*$" , ErrorMessage = "Please use 2 letter capital initial of state")]
        public String State { get; set; }

        [Required]
        public int priceRange { get; set; }
    }
}
