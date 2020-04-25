using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SmartSchoolSystem.Models
{
    public class StudentRegistrationViewModels
    {
        [Required]
        [Display(Name = "First Name")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]
        public string FirstName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid Last Name")]
        public string LastName { get; set; }


        [Required]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }


        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string StdPassword { get; set; }

        [Required]
        [Display(Name = "CNIC/B-Form#")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Must have 13 digits(without dashes).")]
        public string CNIC { get; set; }






    }
}