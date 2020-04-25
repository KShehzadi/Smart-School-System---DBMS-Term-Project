using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSchoolSystem.Models
{
    public class EventViewModel
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Discription")]
        public string description { get; set; }

        [Required]
        [Display(Name = "Contact")]
        public string contact { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        public DateTime startTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        public DateTime endTime { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Number")]
        [Display(Name = "Charges")]
        public string charges { get; set; }

        public static List<EventViewModel> EventList = new List<EventViewModel>();
        public static EventViewModel SampleEvent = new EventViewModel();
    }
}