using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class StudentsLeftViewModel
    {
        public static List<StudentsLeftViewModel> studentList = new List<StudentsLeftViewModel>();

        public int id { get; set; }

        public int studentId { get; set; }

        public string studentName { get; set; }

        public string studentRegNo { get; set; }

        public int classId { get; set; }

        public string className { get; set; }

        public string status { get; set; }
    }
}