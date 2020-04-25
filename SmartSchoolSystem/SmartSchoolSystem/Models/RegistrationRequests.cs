using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class RegistrationRequests
    {
        public static List<RegistrationRequests> RequestList = new List<RegistrationRequests>();

        public string designation { get; set; }
        
        public int id { get; set; }

        public string Name { get; set; }

        public string status { get; set; }
        
    }
}