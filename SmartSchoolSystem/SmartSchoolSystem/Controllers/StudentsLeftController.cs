
using SmartSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSchoolSystem.Controllers
{
    public class StudentsLeftController : Controller
    {
        // GET: StudentsLeft
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewActiveStudents()
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            StudentsLeftViewModel.studentList.Clear();
           
            DB37Entities db = new DB37Entities();
            List<string> myclasslist = new List<string>();
            foreach (Classtbl c in db.Classtbls)
            {
                myclasslist.Add(c.Section);
            }
            ViewBag.Options = myclasslist;

            int activestatusid = db.ActiveStatustbls.Where(t1 => t1.Name == "Active").FirstOrDefault().Id;
            foreach(StudentClasstbl sc in db.StudentClasstbls)
            {
                if(db.Studentstbls.Any(t=> t.ActiveStatusId == activestatusid && t.Id == sc.Id && t.RegistrationNumber != "-1"))
                {
                    StudentsLeftViewModel studentsLeft = new StudentsLeftViewModel();
                    studentsLeft.classId = sc.ClassId;
                    studentsLeft.className = db.Classtbls.Where(t1 => t1.Id == studentsLeft.classId).FirstOrDefault().Section;
                    studentsLeft.studentId = sc.StudentId;
                    studentsLeft.studentName = db.Studentstbls.Where(t2 => t2.Id == studentsLeft.studentId).FirstOrDefault().FirstName + " " + db.Studentstbls.Where(t2 => t2.Id == studentsLeft.studentId).FirstOrDefault().LastName;
                    studentsLeft.studentRegNo = db.Studentstbls.Where(t2 => t2.Id == studentsLeft.studentId).FirstOrDefault().RegistrationNumber;
                    StudentsLeftViewModel.studentList.Add(studentsLeft);
                }
            }
          

            return View(StudentsLeftViewModel.studentList);
        }
        [HttpPost]
        public ActionResult ViewActiveStudents(FormCollection form)
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            StudentsLeftViewModel.studentList.Clear();
            DB37Entities db = new DB37Entities();
            List<string> myclasslist = new List<string>();
            foreach (Classtbl c in db.Classtbls)
            {
                myclasslist.Add(c.Section);
            }
            ViewBag.Options = myclasslist;

            string classname = form["Classlist"].ToString();
            ViewBag.Classname = classname;
            int classid = db.Classtbls.Where(t => t.Section.Equals(classname)).FirstOrDefault().Id;
            int activestatusid = db.ActiveStatustbls.Where(t1 => t1.Name == "Active").FirstOrDefault().Id;
            foreach (StudentClasstbl sc in db.StudentClasstbls)
            {
                if (db.Studentstbls.Any(t => t.ActiveStatusId == activestatusid && t.Id == sc.Id) && sc.ClassId==classid)
                {

                    StudentsLeftViewModel studentsLeft = new StudentsLeftViewModel();
                    studentsLeft.classId = sc.ClassId;
                    studentsLeft.className = db.Classtbls.Where(t1 => t1.Id == studentsLeft.classId).FirstOrDefault().Section;
                    studentsLeft.studentId = sc.StudentId;
                    studentsLeft.studentName = db.Studentstbls.Where(t2 => t2.Id == studentsLeft.studentId).FirstOrDefault().FirstName + " " + db.Studentstbls.Where(t2 => t2.Id == studentsLeft.studentId).FirstOrDefault().LastName;
                    studentsLeft.studentRegNo = db.Studentstbls.Where(t2 => t2.Id == studentsLeft.studentId).FirstOrDefault().RegistrationNumber;
                    StudentsLeftViewModel.studentList.Add(studentsLeft);
                }
            }


            return View(StudentsLeftViewModel.studentList);
        }

        public ActionResult InactiveStudents(int id)
        {
            if(HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            DB37Entities db = new DB37Entities();
            int inactivestatusid = db.ActiveStatustbls.Where(t1 => t1.Name == "Inactive").FirstOrDefault().Id;
            db.Studentstbls.Where(t => t.Id == id).FirstOrDefault().ActiveStatusId = inactivestatusid;
            StudentLefttbl st = new StudentLefttbl();
            st.StudentId = id;
            st.DateLeft = DateTime.Now.Date;
            db.StudentLefttbls.Add(st);
            db.SaveChanges();
            return RedirectToAction("ViewActiveStudents","StudentsLeft");
        }

        // GET: StudentsLeft/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentsLeft/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentsLeft/Create
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

        // GET: StudentsLeft/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentsLeft/Edit/5
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

        // GET: StudentsLeft/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentsLeft/Delete/5
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
