using SmartSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSchoolSystem.Controllers
{
    public class EventsController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {

            EventViewModel.EventList.Clear();
            //Event e1 = new Event();
            //e1.id = 0;
            //e1.name = "satrangi";
            //e1.description = "fazool";
            //e1.contact = "090078601";
            //e1.date = DateTime.Now;


            //Event.EventList.Add(e1);
            //Event.EventList.Add(e1);
            //Event.EventList.Add(e1);
            //Event.EventList.Add(e1);

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

        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult Create(EventViewModel obj)
        {
            try
            {
                if(HelperClass.account != "Admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                // TODO: Add insert logic here
                DB37Entities db = new DB37Entities();

                SchoolEventstbl e1 = new SchoolEventstbl();
                e1.Title = obj.name;
                e1.EventDescription = obj.description;
                e1.EventDate = obj.date;
                e1.StartTime = obj.startTime;
                e1.EndTime = obj.endTime;
                e1.Contact = obj.contact;
                e1.Charges = obj.charges;

                db.SchoolEventstbls.Add(e1);
                db.SaveChanges();


                return RedirectToAction("Index","Events");
            }
            catch (Exception ex)
            {
                throw (ex);
                //return View();
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            DB37Entities db = new DB37Entities();

            EventViewModel eventobj = new EventViewModel();

            eventobj.name = db.SchoolEventstbls.Find(id).Title;
            eventobj.description = db.SchoolEventstbls.Find(id).EventDescription;
            eventobj.contact = db.SchoolEventstbls.Find(id).Contact;
            eventobj.date = db.SchoolEventstbls.Find(id).EventDate;
            eventobj.startTime = db.SchoolEventstbls.Find(id).StartTime;
            eventobj.endTime = db.SchoolEventstbls.Find(id).EndTime;
            eventobj.charges = db.SchoolEventstbls.Find(id).Charges;

            return View(eventobj);
        }

        // POST: Events/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EventViewModel eventObj)
        {
            try
            {
                if (HelperClass.account != "Admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                DB37Entities db = new DB37Entities();

                db.SchoolEventstbls.Find(id).Title = eventObj.name;
                db.SchoolEventstbls.Find(id).EventDescription = eventObj.description;
                db.SchoolEventstbls.Find(id).Contact = eventObj.contact;
                db.SchoolEventstbls.Find(id).EventDate = eventObj.date;
                db.SchoolEventstbls.Find(id).StartTime = eventObj.startTime;
                db.SchoolEventstbls.Find(id).EndTime = eventObj.endTime;
                db.SchoolEventstbls.Find(id).Charges = eventObj.charges;

                db.SaveChanges();

                return RedirectToAction("Index", "Events");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int id)
        {
            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            DB37Entities db = new DB37Entities();

            db.SchoolEventstbls.Remove(db.SchoolEventstbls.Find(id));
            db.SaveChanges();

            return RedirectToAction("Index", "Events");
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
