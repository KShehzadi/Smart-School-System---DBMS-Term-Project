using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSchoolSystem.Models
{
    public class Attendance
    {
        public static List<string> studentlist = new List<string>();
        public static int check;
        public int id { get; set; }
        public string regNo { get; set; }
        public string status { get; set; }
        public List<SelectListItem> statuslist { get; set; }
        public string selectedstatus { get; set; }
    }
   
}