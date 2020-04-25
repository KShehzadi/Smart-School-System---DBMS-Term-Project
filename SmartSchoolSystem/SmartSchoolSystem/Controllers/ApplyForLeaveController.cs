using SmartSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSchoolSystem.Controllers
{
    public class ApplyForLeaveController : Controller
    {
        // GET: ApplyForLeave
        public ActionResult Index()
        {
            return View();
        }

        // GET: ApplyForLeave/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplyForLeave/Create
        public ActionResult Create()
        {

            if (HelperClass.account != "Parent")
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }

        // POST: ApplyForLeave/Create
        [HttpPost]
        public ActionResult Create(ApplyForLeaveViewModel Obj)
        {
            try
            {

                if (HelperClass.account != "Parent")
                {
                    return RedirectToAction("Index", "Home");
                }
                ApplyForLeaveViewModel a = new ApplyForLeaveViewModel();
                if (a.invalidRegistrationNumber(Obj.regNo))
                {
                    ViewBag.warn = "Invalid Registration Number (SSS-X)";
                    return View();
                }
                // TODO: Add insert logic here

                DB37Entities db = new DB37Entities();
                if (db.Parentstbls.Any(t1 => t1.MailAddress.Equals(Obj.parentEmail)) && db.Studentstbls.Any(t2 => t2.RegistrationNumber.Equals(Obj.regNo)))
                {
                    Leavestbl l = new Leavestbl();
                    l.ParentId = db.Parentstbls.Where(t3 => t3.MailAddress.Equals(Obj.parentEmail)).FirstOrDefault().Id;
                    l.StudentId = db.Studentstbls.Where(t4 => t4.RegistrationNumber.Equals(Obj.regNo)).FirstOrDefault().Id;
                    l.LeaveDescription = Obj.description;
                    l.DateFrom = Obj.from;
                    l.DateEnd = Obj.to;
                    l.ApprovalStatusId = db.ApprovalStatustbls.Where(t5 => t5.Name.Equals("pending")).FirstOrDefault().Id;
                    db.Leavestbls.Add(l);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.warn = "Invalid Credentials";
                    return View();
                }
                

                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        // GET: ApplyForLeave/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApplyForLeave/Edit/5
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

        // GET: ApplyForLeave/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApplyForLeave/Delete/5
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
