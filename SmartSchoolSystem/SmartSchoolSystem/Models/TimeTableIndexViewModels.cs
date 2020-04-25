using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class TimeTableIndexViewModels
    {
        public static List<TimeTableIndexViewModels> indextimetable = new List<TimeTableIndexViewModels>();
        public string dayname { get; set; }
        public string subjectname { get; set; }
        public string time { get; set; } 
    }
}