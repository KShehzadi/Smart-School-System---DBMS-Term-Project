using SmartSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSchoolSystem.Controllers
{
    public class ParentController : Controller
    {
        // GET: Parent
        public ActionResult Index()
        {


            return View();
        }



        public ActionResult ParentRegistration()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ParentRegistration(ParentRegistrationViewModels p)
        {
            DB37Entities db = new DB37Entities();
            if(db.Parentstbls.Any(t1=> t1.MailAddress.Equals(p.Email) || db.Parentstbls.Any(t2 => t2.CNIC.Equals(p.CNIC)) || db.Parentstbls.Any(t3 => t3.PhoneNumber.Equals(p.PhoneNumber))))
            {
                ViewBag.Warnning = "Parent with these credentials already exist";
                return View();
            }
            ParentRegistrationViewModels.p.FirstName = p.FirstName;
            ParentRegistrationViewModels.p.LastName = p.LastName ;
            ParentRegistrationViewModels.p.Email = p.Email;
            ParentRegistrationViewModels.p.PhoneNumber = p.PhoneNumber;
            ParentRegistrationViewModels.p.CNIC = p.CNIC;
            ParentRegistrationViewModels.p.PrntPassword = p.PrntPassword;
            

            return RedirectToAction("StudentRegistration","Student");
        }
        // GET: Parent/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Parent/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parent/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Parent/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Parent/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Parent/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Parent/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
