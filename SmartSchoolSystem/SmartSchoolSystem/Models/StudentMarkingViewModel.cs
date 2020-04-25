using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class StudentMarkingViewModel
    {
        public static List<StudentMarkingViewModel> studentsList = new List<StudentMarkingViewModel>();

        public int id { get; set; }

        public int studentId { get; set; }

        public string studentName { get; set; }

        public string studentRegNo { get; set; }

        public string classId { get; set; }

        public string className { get; set; }

        public int subjectId { get; set; }

        public string subjectName { get; set; }

        public int totalMarks { get; set; }

        [Required]
        [RegularExpression("([0-9]+)")]
        public int obtainedMarks { get; set; }
    }
}