using SmartSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace SmartSchoolSystem.Controllers
{
    public class LeaveRequestsController : Controller
    {
        // GET: LeaveRequests
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ApproveLeaveRequests(int id)
        {
            DB37Entities db = new DB37Entities();
            int p = db.ApprovalStatustbls.Where(t1 => t1.Name.Equals("approved")).FirstOrDefault().Id;
            db.Leavestbls.Where(t2 => t2.ParentId.Equals(id)).FirstOrDefault().ApprovalStatusId = db.ApprovalStatustbls.Where(t1 => t1.Name.Equals("approved")).FirstOrDefault().Id;
            //int pid = db.Leavestbls.Find(id).ParentId;
            SendApprovalMail(db.Parentstbls.Find(id).MailAddress);
            db.SaveChanges();
            

            return RedirectToAction("ViewLeaveRequests", "LeaveRequests");
        }

        public ActionResult DisapproveLeaveRequests(int id)
        {
            try
            {
                DB37Entities db = new DB37Entities();

                foreach (Leavestbl ltbl in db.Leavestbls)
                {
                    if (ltbl.ParentId == id)
                    {
                        db.Leavestbls.Remove(ltbl);
                        break;
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
            //int pid = db.Leavestbls.Find(id).ParentId;
            
            //SendDisapprovalMail(db.Parentstbls.Find(id).MailAddress);

            //db.Leavestbls.Find(id).ApprovalStatusId = db.ApprovalStatustbls.Where(t1 => t1.Name.Equals("approved")).FirstOrDefault().Id;
            //int pid = db.Leavestbls.Find(id).ParentId;
            //SendApprovalMail(db.Parentstbls.Find(pid).MailAddress);
            //db.SaveChanges();


            return RedirectToAction("ViewLeaveRequests", "LeaveRequests");
        }

        public ActionResult ViewLeaveRequests()
        {
            LeaveRequestsViewModel.requests.Clear();
            DB37Entities db = new DB37Entities();

            foreach (Leavestbl ltbl in db.Leavestbls)
            {
                if (ltbl.ApprovalStatusId == db.ApprovalStatustbls.Where(t1 => t1.Name.Equals("pending")).FirstOrDefault().Id)
                {
                    LeaveRequestsViewModel leaveRequest = new LeaveRequestsViewModel();
                    leaveRequest.parentId = ltbl.ParentId;
                    leaveRequest.parentEmail = db.Parentstbls.Where(t2 => t2.Id.Equals(ltbl.ParentId)).FirstOrDefault().MailAddress;
                    leaveRequest.studentId = ltbl.StudentId;
                    leaveRequest.studentName = db.Studentstbls.Where(t3 => t3.Id.Equals(ltbl.StudentId)).FirstOrDefault().Username;
                    leaveRequest.leaveDescription = ltbl.LeaveDescription;
                    leaveRequest.regNo = db.Studentstbls.Where(t4 => t4.Id.Equals(ltbl.StudentId)).FirstOrDefault().RegistrationNumber;
                    leaveRequest.dateFrom = ltbl.DateFrom;
                    leaveRequest.dateTo = ltbl.DateEnd;
                    LeaveRequestsViewModel.requests.Add(leaveRequest);
                }
            }
            
            return View(LeaveRequestsViewModel.requests);
        }

        // GET: LeaveRequests/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LeaveRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveRequests/Create
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

        // GET: LeaveRequests/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequests/Edit/5
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

        // GET: LeaveRequests/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequests/Delete/5
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
        public void SendApprovalMail(string email)
        {
            string from1 = "smartschoolsystem0@gmail.com";
            GmailViewModel g = new GmailViewModel();
            string email1 = g.email;

            using (MailMessage mail = new MailMessage(from1, email))
            {
                mail.Subject = "Leave Request";
                mail.Body = "You leave is approved";
                //mail.Body = mail.Body.Replace("@", System.Environment.NewLine);
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
                ViewData["Message"] = "Your leave request is approved.";
            }
        }


        [NonAction]
        public void SendDisapprovalMail(string email)
        {
            string from1 = "smartschoolsystem0@gmail.com";
            GmailViewModel g = new GmailViewModel();
            string email1 = g.email;

            using (MailMessage mail = new MailMessage(from1, email))
            {
                mail.Subject = "Leave Request";
                mail.Body = "Your leave request is denied";
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
                ViewData["Message"] = "Your leave request has been denied.";
            }
        }
    }
}
