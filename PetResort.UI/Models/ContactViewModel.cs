using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PetResort.UI.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "*Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Required")]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$",
            ErrorMessage = "Please Enter a Valid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "*Required")]
        public string Message { get; set; }
    }
}