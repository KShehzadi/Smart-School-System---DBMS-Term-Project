using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace SmartSchoolSystem.Models
{
    public class RegistrationRequestsViewModel
    {
        public static List<RegistrationRequestsViewModel> RequestList = new List<RegistrationRequestsViewModel>();

        public int id { get; set; }

        public int parentId { get; set; }

        public string parentName { get; set; }

        public int studentId { get; set; }

        public string studentName { get; set; }

        //public string status { get; set; }




    }
}