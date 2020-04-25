using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class LeaveRequestsViewModel
    {
        public static List<LeaveRequestsViewModel> requests = new List<LeaveRequestsViewModel>();

        public int id { get; set; }

        public int parentId { get; set; }

        public string parentEmail { get; set; }

        public int studentId { get; set; }

        public string studentName { get; set; }

        public string leaveDescription { get; set; }

        public string regNo { get; set; }

        public DateTime dateFrom { get; set; }

        public DateTime dateTo { get; set; }
    }
}