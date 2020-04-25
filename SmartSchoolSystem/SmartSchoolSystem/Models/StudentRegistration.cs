using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SmartSchoolSystem.Models
{
    public class StudentRegistration
    {
        [Required]
        [Display(Name = "First Name")]


        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]

        public string LastName { get; set; }
        [Required]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }


        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string StdPassword { get; set; }


        public string CNIC { get; set; }






    }
}