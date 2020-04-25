using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class ParentRegistrationViewModels
    {
        public static ParentRegistrationViewModels p = new ParentRegistrationViewModels();
        [Required]
        [Display(Name = "First Name")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contact")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Must be at least 11 digits long(Start with 0, do not use +92).")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Must have 13 digits(without dashes).")]
        public string CNIC { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string PrntPassword { get; set; }
    }
}