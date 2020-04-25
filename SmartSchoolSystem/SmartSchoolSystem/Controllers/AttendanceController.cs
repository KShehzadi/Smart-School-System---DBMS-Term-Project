using SmartSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSchoolSystem.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddAttendance()
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            DB37Entities db = new DB37Entities();
            List<string> myclasslist = new List<string>();
            foreach (Classtbl c in db.Classtbls)
            {
                myclasslist.Add(c.Section);
            }
            ViewBag.Options = myclasslist;
            List<string> mystatus = new List<string>();
            
            
            ViewBag.status = mystatus;
            List<Attendance> mylist = new List<Attendance>();
            mylist.Clear();
            ViewBag.myModel = mylist;
           
            return View(mylist);
        }
        public void SetSubmit()
        {
            Attendance.check = 1;
        }
        public void SetFetch()
        {
            Attendance.check = 0;
        }
        [HttpPost]
        public ActionResult AddAttendance(FormCollection form)
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            List<Attendance> list = new List<Attendance>();
            
            DB37Entities db = new DB37Entities();
            List<string> myclasslist = new List<string>();
            foreach (Classtbl c in db.Classtbls)
            {
                myclasslist.Add(c.Section);
            }
            ViewBag.Options = myclasslist;
            List<string> mystatus = new List<string>();
            foreach (AttendanceStatustbl a in db.AttendanceStatustbls)
            {
                mystatus.Add(a.Name);
            }
            ViewBag.status = mystatus;
            int id = Attendance.check;
            string classname = form["Classlist"].ToString();
            ViewBag.Classname = classname;
            int classid = db.Classtbls.Where(t => t.Section.Equals(classname)).FirstOrDefault().Id;
            if (id == 0)
            {
                Attendance.studentlist.Clear();
                foreach (StudentClasstbl s in db.StudentClasstbls)
                {
                    int studentactivestatusid = db.Studentstbls.Where(t => t.Id == s.StudentId).FirstOrDefault().ActiveStatusId;
                    if(s.ClassId == classid && studentactivestatusid == db.ActiveStatustbls.Where(t1=>t1.Name =="Active").FirstOrDefault().Id)
                    {
                        Attendance a = new Attendance();
                        a.regNo = db.Studentstbls.Where(t=> t.Id == s.StudentId).FirstOrDefault().RegistrationNumber;
                        a.statuslist = new List<SelectListItem> {

                            new SelectListItem { Value = db.AttendanceStatustbls.Where(t=>t.Name == "Present").FirstOrDefault().Id.ToString(), Text = "Present" },


                            new SelectListItem { Value =  db.AttendanceStatustbls.Where(t=>t.Name == "Absent").FirstOrDefault().Id.ToString(), Text = "Absent" },

                            new SelectListItem { Value =  db.AttendanceStatustbls.Where(t=>t.Name == "Leave").FirstOrDefault().Id.ToString(), Text = "Leave" },
                        };
                        Attendance.studentlist.Add(a.regNo);
                        list.Add(a);
                    }
                    
                }
                
                return View(list);
            }
            else
            {
                string[] keys = form.AllKeys;
                var added = form[keys[3]].Split(',');
                int i = 0;
                string date = form["date"].ToString();
                classname = form["Classlist"].ToString();
                classid = db.Classtbls.Where(t => t.Section == classname).FirstOrDefault().Id;
                DateTime adate = Convert.ToDateTime(date);
                foreach(AttendanceMarkingtbl amt in db.AttendanceMarkingtbls)
                {
                    DateTime amtl = amt.AttendanceDate;
                    if (amtl.Date == adate.Date)
                    {
                        foreach (StudentClasstbl c in db.StudentClasstbls)
                        {
                            if (c.ClassId == classid && db.AttendanceMarkingtbls.Any(t1 => t1.StudentId == c.StudentId))
                            {
                                Attendance.studentlist.Clear();
                                ViewBag.warn = "Attendance of class at this date is already marked";
                                foreach (StudentClasstbl s in db.StudentClasstbls)
                                {
                                    int studentactivestatusid = db.Studentstbls.Where(t => t.Id == s.StudentId).FirstOrDefault().ActiveStatusId;

                                    if (s.ClassId == classid && studentactivestatusid == db.ActiveStatustbls.Where(t1 => t1.Name == "Active").FirstOrDefault().Id)
                                    {
                                        Attendance a = new Attendance();
                                        a.regNo = db.Studentstbls.Where(t => t.Id == s.StudentId).FirstOrDefault().RegistrationNumber;
                                        a.statuslist = new List<SelectListItem> {


                                            new SelectListItem { Value = db.AttendanceStatustbls.Where(t=>t.Name == "Present").FirstOrDefault().Id.ToString(), Text = "Present" },



                                            new SelectListItem { Value =  db.AttendanceStatustbls.Where(t=>t.Name == "Absent").FirstOrDefault().Id.ToString(), Text = "Absent" },


                                            new SelectListItem { Value =  db.AttendanceStatustbls.Where(t=>t.Name == "Leave").FirstOrDefault().Id.ToString(), Text = "Leave" },
                        };
                                            Attendance.studentlist.Add(a.regNo);
                                            list.Add(a);
                                    }

                                }
                                return View(list);
                            }
                        }
                    }
                }
                i = 0;
                foreach (string regnumber in Attendance.studentlist)
                {
                    var p = added[i];
                    int studentid = db.Studentstbls.Where(t => t.RegistrationNumber == regnumber).FirstOrDefault().Id;
                    int attendanceid = Convert.ToInt32(p);
                    AttendanceMarkingtbl atm = new AttendanceMarkingtbl();
                    atm.StudentId = studentid;
                    atm.AttendanceStatusId = attendanceid;
                    atm.AttendanceDate = adate.Date;
                    db.AttendanceMarkingtbls.Add(atm);
                    i = i + 1;
                }
                db.SaveChanges();
                return RedirectToAction("AddAttendance", "Attendance");
            }
           
      

            
           
        }

        // GET: Attendance/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Attendance/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attendance/Create
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

        // GET: Attendance/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Attendance/Edit/5
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

        // GET: Attendance/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Attendance/Delete/5
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
