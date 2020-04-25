using SmartSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace SmartSchoolSystem.Controllers
{
    public class RegistrationRequestsController : Controller
    {
        // GET: RegistrationRequests
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApproveRegistrationRequests(int id, int sid)
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            DB37Entities db = new DB37Entities();

            //int sid = db.ParentStudenttbls.Where(t => t.ParentId.Equals(id)).FirstOrDefault().StudentId;

            int i = 1;

            foreach (Studentstbl stbl in db.Studentstbls)
            {
                if (stbl.RegistrationNumber == "SSS-" + i.ToString())
                {
                    i++;
                }
            }

            string userName = db.Studentstbls.Find(sid).Username;
            db.Parentstbls.Find(id).ApprovalStatusId = 2;
            db.Studentstbls.Find(sid).ApprovalStatusId = 2;
            db.Studentstbls.Find(sid).RegistrationNumber = "SSS-"+i.ToString();
            int check = SendApprovalMail(db.Parentstbls.Find(id).MailAddress,"SSS-"+ i.ToString(), userName);
            if(check == 0)
            {
                foreach (ParentStudenttbl pstbl in db.ParentStudenttbls)
                {
                    if (pstbl.ParentId == id)
                    {
                        db.ParentStudenttbls.Remove(pstbl);
                    }
                }


                foreach (Studentstbl stbl in db.Studentstbls)
                {
                    if (stbl.Id == sid)
                    {
                        db.Studentstbls.Remove(stbl);
                    }
                }

                foreach (Parentstbl ptbl in db.Parentstbls)
                {
                    if (ptbl.Id == id)
                    {
                        db.Parentstbls.Remove(ptbl);
                    }
                }
            }
            db.SaveChanges();

            return RedirectToAction("ViewRegistrationRequests", "RegistrationRequests");
        }



        public ActionResult DisapproveApproveRegistrationRequests(int id, int sid)
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            DB37Entities db = new DB37Entities();

            //int sid = db.ParentStudenttbls.Where(t => t.ParentId.Equals(id)).FirstOrDefault().StudentId;

            int check = SendDisapprovalMail(db.Parentstbls.Find(id).MailAddress);
            if (check == 0)
            {
                foreach (ParentStudenttbl pstbl in db.ParentStudenttbls)
                {
                    if (pstbl.ParentId == id)
                    {
                        db.ParentStudenttbls.Remove(pstbl);
                    }
                }


                foreach (Studentstbl stbl in db.Studentstbls)
                {
                    if (stbl.Id == sid)
                    {
                        db.Studentstbls.Remove(stbl);
                    }
                }

                foreach (Parentstbl ptbl in db.Parentstbls)
                {
                    if (ptbl.Id == id)
                    {
                        db.Parentstbls.Remove(ptbl);
                    }
                }
                
            }
            else
            { 
            foreach (ParentStudenttbl pstbl in db.ParentStudenttbls)
            {
                if (pstbl.ParentId == id)
                {
                    db.ParentStudenttbls.Remove(pstbl);
                }
            }


            foreach (Studentstbl stbl in db.Studentstbls)
            {
                if (stbl.Id == sid)
                {
                    db.Studentstbls.Remove(stbl);
                }
            }

            foreach (Parentstbl ptbl in db.Parentstbls)
            {
                if (ptbl.Id == id)
                {
                    db.Parentstbls.Remove(ptbl);
                }
            }
            db.SaveChanges();

        }

            return RedirectToAction("ViewRegistrationRequests", "RegistrationRequests");
        }





        public ActionResult ViewRegistrationRequests()
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            RegistrationRequestsViewModel.RequestList.Clear();

            DB37Entities db = new DB37Entities();
            int studentid, parentid;
            foreach (ParentStudenttbl pstbl in db.ParentStudenttbls)
            {
                studentid = pstbl.StudentId;
                parentid = pstbl.ParentId;
                if (db.ApprovalStatustbls.Where(t1 => t1.Id ==(db.Studentstbls.Where(t=>t.Id == studentid).FirstOrDefault().ApprovalStatusId)).FirstOrDefault().Name == "pending")
                {
                    RegistrationRequestsViewModel regReqObj = new RegistrationRequestsViewModel();
                    regReqObj.parentId = pstbl.ParentId;
                    regReqObj.parentName = db.Parentstbls.Find(pstbl.ParentId).MailAddress;
                    regReqObj.studentId = pstbl.StudentId;
                    regReqObj.studentName = db.Studentstbls.Find(pstbl.StudentId).Username;

                    RegistrationRequestsViewModel.RequestList.Add(regReqObj);
                }
            }

            //RegistrationRequestsViewModel requests1 = new RegistrationRequestsViewModel();
            //requests1.id = 1;
            //requests1.parentId = 1;
            //requests1.parentName = "Abc";
            //requests1.studentId = 1;
            //requests1.studentName = "Def";
            
            //RegistrationRequestsViewModel.RequestList.Add(requests1);
            

            return View(RegistrationRequestsViewModel.RequestList);
        }

        // GET: RegistrationRequests/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegistrationRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistrationRequests/Create
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

        // GET: RegistrationRequests/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegistrationRequests/Edit/5
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

        // GET: RegistrationRequests/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegistrationRequests/Delete/5
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

        [NonAction]
        public int SendApprovalMail(string email, string regNo, string userName)
        {
            try
            {

                
                string from1 = "smartschoolsystem0@gmail.com";
                GmailViewModel g = new GmailViewModel();
                string email1 = g.email;

                using (MailMessage mail = new MailMessage(from1, email))
                {
                    mail.Subject = "Registration Request";
                    mail.Body = "Congratulations! Your request has been accepted. You can now log in to your account.@Your child with username: " + userName + " can now login to his account.@Registration Number assigned to your child is " + regNo;
                    mail.Body = mail.Body.Replace("@", System.Environment.NewLine);
                    mail.IsBodyHtml = false;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from1, "smart*123");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                    ViewBag.Message = "Sent";
                    ViewData["Message"] = "Your request has been accepted you can now login.";
                    return 1;
                }
            }
            catch
            {
                return 0;
            }
        }


        [NonAction]
        public int SendDisapprovalMail(string email)
        {
            try
            { 
            string from1 = "smartschoolsystem0@gmail.com";
            GmailViewModel g = new GmailViewModel();
            string email1 = g.email;

            using (MailMessage mail = new MailMessage(from1, email))
            {
                mail.Subject = "Registration Request";
                mail.Body = "Your request to register has been denied by school administration.";
                mail.Body = mail.Body.Replace("@", System.Environment.NewLine);
                mail.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential(from1, "smart*123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.Send(mail);
                ViewBag.Message = "Sent";
                ViewData["Message"] = "Your request has been denied.";

            }
            return 1;
        }
            catch
            {
                return 0;
            }
        }
    }
}
