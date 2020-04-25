using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class AssessmentViewModel
    {
        public static List<AssessmentViewModel> assessmentList = new List<AssessmentViewModel>();

        public static bool flag = false;

        public static int classId { get; set; }

        public static int subjectId { get; set; }

        public static int termId { get; set; }

        public string id { get; set; }

        public int studentId { get; set; }

        public string regNo { get; set; }

        public int obtainedMarks { get; set; }

        public static int totalMarks { get; set; }
    }
}