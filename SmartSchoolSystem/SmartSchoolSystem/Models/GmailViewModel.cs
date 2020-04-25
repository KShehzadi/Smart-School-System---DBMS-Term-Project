using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class GmailViewModel
    {
        public string from { set; get; }

        public string to { set; get; }

        public string subject { set; get; }

        public string email { get; set; }

        public string body { set; get; }
    }
}