using SmartSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSchoolSystem.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(LogInViewModels login)
        {
            DB37Entities db = new DB37Entities();
            if(HelperClass.account == "Admin")
            {
                if(db.Adminstratortbls.Any(t1 => t1.Username.Equals(login.UserName) && db.Adminstratortbls.Any(t2 => t2.AdminPassword.Equals(login.Password))))
                   {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HelperClass.account = "";
                    ViewBag.warn = "Admin with these credentials does not exist";
                }
            }
            else if(HelperClass.account == "Parent")
            {
                if (db.Parentstbls.Any(t1 => t1.MailAddress.Equals(login.UserName) && 
                db.Parentstbls.Where(t4=> t4.MailAddress.Equals(login.UserName)).FirstOrDefault().PrntPassword.Equals(login.Password)) && 
                    db.Parentstbls.Where(t2 => t2.MailAddress.Equals(login.UserName)).FirstOrDefault().ApprovalStatusId.Equals(db.ApprovalStatustbls.Where(t3 => t3.Name.Equals("approved")).FirstOrDefault().Id))
                {
                    HelperClass.parentid = db.Parentstbls.Where(t => t.MailAddress == login.UserName).First().Id;
                    HelperClass.accountid = HelperClass.parentid;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HelperClass.account = "";
                    ViewBag.warn = "Parent with these credentials does not exist";
                }
            }
            else if (HelperClass.account == "Student")
            {
                if (db.Studentstbls.Any(t1 => t1.Username.Equals(login.UserName) &&
                db.Studentstbls.Where(t4 => t4.Username.Equals(login.UserName)).FirstOrDefault().StdPassword.Equals(login.Password)) &&
                    db.Studentstbls.Where(t2 => t2.Username.Equals(login.UserName)).FirstOrDefault().ApprovalStatusId.Equals(db.ApprovalStatustbls.Where(t3 => t3.Name.Equals("approved")).FirstOrDefault().Id))
                {
                    HelperClass.accountid = db.Studentstbls.Where(t => t.Username == login.UserName).First().Id;
                    HelperClass.studentregno = db.Studentstbls.Where(t => t.Id == HelperClass.accountid).FirstOrDefault().RegistrationNumber;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HelperClass.account = "";
                    ViewBag.warn = "Student with these credentials does not exist";
                }
            }
            return View();
        }

        public int SetAdmin()
        {
            HelperClass.account = "Admin";
            return 0;


        }
        public int SetStudent()
        {
            HelperClass.account = "Student";
            return 0;

        }
        public int SetParent()
        {
            HelperClass.account = "Parent";
            return 0;

        }
        public int SetEmpty()
        {
            HelperClass.account = "";
            return 0;


        }
        // GET: LogIn/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LogIn/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogIn/Create
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

        // GET: LogIn/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LogIn/Edit/5
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

        // GET: LogIn/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LogIn/Delete/5
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
        public ActionResult LogOut()
        {
            HelperClass.account = "";
            HelperClass.parentid = -1;
            HelperClass.accountid = -1;
            return RedirectToAction("Index", "Home");
        }

        
        public ActionResult LogInPost(LogInViewModels login)
        {
            
            try
            {
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult LogInRequest(LogInViewModels login,string account)
        {
            return View();
        }
     

    }
}
