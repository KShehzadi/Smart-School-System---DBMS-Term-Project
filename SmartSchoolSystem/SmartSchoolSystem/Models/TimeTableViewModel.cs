using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class TimeTableViewModel
    {
        public static List<TimeTableViewModel> lst = new List<TimeTableViewModel>();

        public int id { get; set; }

        public string subject { get; set; }

        public string day { get; set; }
    }
}