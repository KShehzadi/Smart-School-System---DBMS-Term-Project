using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class LogInViewModels
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public accounts accountname { get; set; }
        
    }
    public enum accounts
    {
        Admin,
        Parent,
        Student
    }
}