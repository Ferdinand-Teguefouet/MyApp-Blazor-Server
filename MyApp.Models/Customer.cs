using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models
{
    public class Customer
    {
        [Required (ErrorMessage = "Ce champ est obligatoire!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Le numéro de rue doit être un nombre entier!"), RegularExpression(pattern: "^[0-9]*$")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Ce champ est obligatoire!"), StringLength(maximumLength: 64, MinimumLength = 1)]
        public string Street { get; set; }

        [Required(ErrorMessage = "Le code postal dait contenir 4 chiffres!"), RegularExpression(pattern: "^[0-9]{4}$")]
        public string Zipcode { get; set; }
    }
}
