using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class ClassesViewModels
    {
        public static ClassesViewModels sampleclass = new ClassesViewModels();
        public static List<ClassesViewModels> ClassesList = new List<ClassesViewModels>();
        private int id;

        [Required]
        [Display(Name = "Name")]
        private string name;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }
}