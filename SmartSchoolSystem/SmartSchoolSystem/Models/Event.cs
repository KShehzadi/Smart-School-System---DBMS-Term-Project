using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class Event
    {


        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string contact { get; set; }

        [DataType(DataType.Date)]
        public DateTime date { get; set; }
        public static List<Event> EventList = new List<Event>();
        public static Event SampleEvent = new Event();
    }
}