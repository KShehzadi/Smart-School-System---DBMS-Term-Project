using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class TimeTableDetailedViewModels
    {
        public static string classname{get; set;}
        public static List<TimeTableDetailedViewModels> timetablelist = new List<TimeTableDetailedViewModels>();
        public string dayname { get; set; }
        public string lecture1 { get; set; }
        public string lecture2 { get; set; }
        public string lecture3 { get; set; }
        public string lecture4 { get; set; }
        public string lecture5 { get; set; }
    }
}