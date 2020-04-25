using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class LeaveRequests
    {
        public static List<LeaveRequests> requests = new List<LeaveRequests>();

        public int id { get; set; }

        public string name { get; set; }

        public string regNo { get; set; }

        public DateTime dateFrom { get; set; }

        public DateTime dateTo { get; set; }

        public string status { get; set; }
    }
}