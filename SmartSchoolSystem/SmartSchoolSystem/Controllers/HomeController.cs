using SmartSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSchoolSystem.Controllers
{
   
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            EventViewModel.EventList.Clear();

            DB37Entities db = new DB37Entities();

            //List<EventViewModel> eventLst = new List<EventViewModel>();

            foreach (SchoolEventstbl e in db.SchoolEventstbls)
            {
                EventViewModel eventObj = new EventViewModel();
                eventObj.name = e.Title;
                eventObj.description = e.EventDescription;
                eventObj.contact = e.Contact;
                eventObj.date = e.EventDate;
                eventObj.startTime = e.StartTime;
                eventObj.endTime = e.EndTime;
                eventObj.charges = e.Charges;
                eventObj.id = e.Id;
                EventViewModel.EventList.Add(eventObj);
            }



            return View(EventViewModel.EventList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}