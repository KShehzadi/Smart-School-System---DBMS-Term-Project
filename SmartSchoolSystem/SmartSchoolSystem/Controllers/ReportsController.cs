using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSchoolSystem.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public static string classname;
        public static string studentreg;
        public static string termname;
        public ActionResult Index()
        {
            return View();
        }

        // GET: Reports/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult ParentReports()
        {
            List<string> students = new List<string>();
            DB37Entities db = new DB37Entities();
            foreach (ParentStudenttbl c in db.ParentStudenttbls)
            {
                if (c.ParentId == HelperClass.accountid)
                {
                    students.Add(db.Studentstbls.Where(t => t.Id == c.StudentId).FirstOrDefault().RegistrationNumber);
                }
            }
            ViewBag.students = students;
            
            List<string> termList = new List<string>();
            foreach (Termstbl ttbl in db.Termstbls)
            {
                termList.Add(ttbl.Name);
            }
            ViewBag.Options1 = termList;

            return View();

        }
        public ActionResult StudentReports()
        {
            DB37Entities db = new DB37Entities();
            

            List<string> termList = new List<string>();
            foreach (Termstbl ttbl in db.Termstbls)
            {
                termList.Add(ttbl.Name);
            }
            ViewBag.Options1 = termList;


            return View();

        }
        public ActionResult AllStudents()
        {
            DB37Entities db = new DB37Entities();
            //CrMVCApp.Models.Customer c;
            var c = db.School_Students.ToList();

            SchoolStudentsReport  rpt = new SchoolStudentsReport();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult ClassWiseStudents()
        {
            string classanme = ReportsController.classname;
            if (classname == "All")
            {
                return RedirectToAction("AllStudents", "Reports");
            }
            

            DB37Entities db = new DB37Entities();
           

            var c = db.StudentsOfClass(classname).ToList();

            StudentsOfClass rpt = new StudentsOfClass();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult ActiveStudents()
        {
            DB37Entities db = new DB37Entities();
            //CrMVCApp.Models.Customer c;
            var c = db.ShowAllActiveStudents.ToList();

            ActiveStudentsReport rpt = new ActiveStudentsReport();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult InActiveStudents()
        {
            DB37Entities db = new DB37Entities();
            //CrMVCApp.Models.Customer c;
            var c = db.ShowAllInActiveStudents.ToList();

            InActiveStudentsReport rpt = new InActiveStudentsReport();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult JsonSuccess(string classname)
        {
            ReportsController.classname = classname;
            return Json(new { r = "success" });
        }
        public ActionResult JsonSuccess2(string studentname)
        {
            if(HelperClass.account == "Student")
            {
                ReportsController.studentreg = HelperClass.studentregno;
            }
            else
            {

                ReportsController.studentreg = studentname;

            }
            return Json(new { r = "success" });
        }
        public ActionResult JsonSuccess3(string studentname, string termname)
        {
            if (HelperClass.account == "Student")
            {
                ReportsController.studentreg = HelperClass.studentregno;
            }
            else
            {

                ReportsController.studentreg = studentname;

            }
            ReportsController.termname = termname;
            return Json(new { r = "success" });
        }
        public ActionResult JsonSuccess4(string classname, string termname)
        {
            ReportsController.classname = classname;
            ReportsController.termname = termname;
            return Json(new { r = "success" });
        }
        public ActionResult ClassWiseActiveStudents()
        {
            string classname = ReportsController.classname;
            if(classname == "All")
            {
                return RedirectToAction("ActiveStudents", "Reports");
            }
            int classid = 1;
            DB37Entities db = new DB37Entities();
            classid = db.Classtbls.Where(t => t.Section == classname).FirstOrDefault().Id;

            //CrMVCApp.Models.Customer c;
            var c = db.ShowClassWiseActiveStudents(classid).ToList();

            ActiveStudentsReport rpt = new ActiveStudentsReport();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

        public ActionResult StudentTermWiseMarks()
        {
            string classname = ReportsController.classname;
            if (classname == "All")
            {
                return RedirectToAction("AdminReportsOnly","Reports");
            }
            int studentid = 1;
            int termid = 0;
            DB37Entities db = new DB37Entities();
            studentid = db.Studentstbls.Where(t => t.RegistrationNumber == ReportsController.studentreg).FirstOrDefault().Id;
            termid = db.Termstbls.Where(t => t.Name == ReportsController.termname).FirstOrDefault().Id;

            //CrMVCApp.Models.Customer c;
            var c = db.TermWiseMarksOfEachStudent(studentid,termid).ToList();

            TermWiseMarks rpt = new TermWiseMarks();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult StudentAllTermMarks()
        {
            string classname = ReportsController.classname;
            if (classname == "All")
            {
                return RedirectToAction("AdminReportsOnly", "Reports");
            }
            int studentid = 1;
          
            DB37Entities db = new DB37Entities();
            studentid = db.Studentstbls.Where(t => t.RegistrationNumber == ReportsController.studentreg).FirstOrDefault().Id;
            

            //CrMVCApp.Models.Customer c;
            var c = db.AllTermMarksOfEachStudent(studentid).ToList();

            TermWiseMarks rpt = new TermWiseMarks();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

        public ActionResult ClassTermWiseMarks()
        {
            string classname = ReportsController.classname;
            if (classname == "All")
            {
                return RedirectToAction("AdminReportsOnly", "Reports");
            }
            int classid = 1;
            int termid = 0;
            DB37Entities db = new DB37Entities();
            classid = db.Classtbls.Where(t=> t.Section == ReportsController.classname).FirstOrDefault().Id;
            termid = db.Termstbls.Where(t => t.Name == ReportsController.termname).FirstOrDefault().Id;

            //CrMVCApp.Models.Customer c;
            var c = db.TermWiseMarksOfAClass(classid,termid).ToList();

            TermWiseMarks rpt = new TermWiseMarks();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

        public ActionResult ClassAllTermMarks()
        {
            string classname = ReportsController.classname;
            if (classname == "All")
            {
                return RedirectToAction("AdminReportsOnly", "Reports");
            }
            int classid = 1;
          
            DB37Entities db = new DB37Entities();
            classid = db.Classtbls.Where(t => t.Section == ReportsController.classname).FirstOrDefault().Id;
            
            //CrMVCApp.Models.Customer c;
            var c = db.AllTermMarksOfAClass(classid).ToList();

            TermWiseMarks rpt = new TermWiseMarks();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult ClassTimeTable()
        {
            string classname = ReportsController.classname;
            if (classname == "All")
            {
                return RedirectToAction("AdminReportsOnly", "Reports");
            }
            int classid = 1;

            DB37Entities db = new DB37Entities();
            classid = db.Classtbls.Where(t => t.Section == ReportsController.classname).FirstOrDefault().Id;

            //CrMVCApp.Models.Customer c;
            var c = db.TimeTableForEachClass(classid).ToList();

            TimeTableForEachClass rpt = new TimeTableForEachClass();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult DetailedClassMarking()
        {
            string classname = ReportsController.classname;
            if (classname == "All")
            {
                return RedirectToAction("AdminReportsOnly", "Reports");
            }
            int classid = 1;

            DB37Entities db = new DB37Entities();
            classid = db.Classtbls.Where(t => t.Section == ReportsController.classname).FirstOrDefault().Id;

            //CrMVCApp.Models.Customer c;
            var c = db.SubjectWiseMarksForClass(classid).ToList();

            DetailedClassAssessment rpt = new DetailedClassAssessment();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

        public ActionResult StudentSubjects()
        {
            string studentreg = ReportsController.studentreg;
            
            int studentid = 1;
            DB37Entities db = new DB37Entities();
            if(db.Studentstbls.Any(t=> t.RegistrationNumber == studentreg))
            {
                studentid = db.Studentstbls.Where(t => t.RegistrationNumber == studentreg).FirstOrDefault().Id;
            }
            else
            {
                ViewBag.warn = "Invalid Student Registration Number";
                return RedirectToAction("AdminReportsOnly");
            }
         

            //CrMVCApp.Models.Customer c;
            var c = db.SubjectsOfStudent(studentid).ToList();

            SubjectsOfStudent rpt = new SubjectsOfStudent();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult StudentAttendance()
        {
            string studentreg = ReportsController.studentreg;

            int studentid = 1;
            DB37Entities db = new DB37Entities();
            if (db.Studentstbls.Any(t => t.RegistrationNumber == studentreg))
            {
                studentid = db.Studentstbls.Where(t => t.RegistrationNumber == studentreg).FirstOrDefault().Id;
            }
            else
            {
                ViewBag.warn = "Invalid Student Registration Number";
                return RedirectToAction("AdminReportsOnly");
            }


            //CrMVCApp.Models.Customer c;
            var c = db.AttendanceofEachStudent(studentid).ToList();

            AttendanceOfEachStudent rpt = new AttendanceOfEachStudent();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult DetailedStudentAssessment()
        {
            string studentreg = ReportsController.studentreg;

            int studentid = 1;
            DB37Entities db = new DB37Entities();
            if (db.Studentstbls.Any(t => t.RegistrationNumber == studentreg))
            {
                studentid = db.Studentstbls.Where(t => t.RegistrationNumber == studentreg).FirstOrDefault().Id;
            }
            else
            {
                ViewBag.warn = "Invalid Student Registration Number";
                return RedirectToAction("AdminReportsOnly");
            }


            //CrMVCApp.Models.Customer c;
            var c = db.SubjectWiseMarkingForEachStudent(studentid).ToList();

            DetailedStudentAssessment rpt = new DetailedStudentAssessment();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult StudentInfo()
        {
            string studentreg = ReportsController.studentreg;

            int studentid = 1;
            DB37Entities db = new DB37Entities();
            if (db.Studentstbls.Any(t => t.RegistrationNumber == studentreg))
            {
                studentid = db.Studentstbls.Where(t => t.RegistrationNumber == studentreg).FirstOrDefault().Id;
            }
            else
            {
                ViewBag.warn = "Invalid Student Registration Number";
                return RedirectToAction("AdminReportsOnly");
            }


            //CrMVCApp.Models.Customer c;
            var c = db.ShowStudentInfo(studentid).ToList();

            StudentInfo rpt = new StudentInfo();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult ClassAttendance()
        {
            string classname = ReportsController.classname;

            int classid = 1;
            DB37Entities db = new DB37Entities();
            if (classname == "All")
            {
                ViewBag.warn = "Invalid Student Registration Number";
                return RedirectToAction("AdminReportsOnly");
               
            }


            classid = db.Classtbls.Where(t => t.Section == classname).FirstOrDefault().Id;
            //CrMVCApp.Models.Customer c;
            var c = db.AttendanceofWholeClass(classid).ToList();

            ClassAttendanceReport rpt = new ClassAttendanceReport();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult StudentTotalMarks()
        {
            string studentreg = ReportsController.studentreg;

            int studentid = 1;
            DB37Entities db = new DB37Entities();
            if (db.Studentstbls.Any(t => t.RegistrationNumber == studentreg))
            {
                studentid = db.Studentstbls.Where(t => t.RegistrationNumber == studentreg).FirstOrDefault().Id;
            }
            else
            {
                ViewBag.warn = "Invalid Student Registration Number";
                return View("AdminReportsOnly");
            }


            //CrMVCApp.Models.Customer c;
            var c = db.TotalMarksOfEachStudent(studentid).ToList();

            StudentTotalMarks rpt = new StudentTotalMarks();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult ClassWiseInActiveStudents()
        {
            string classanme = ReportsController.classname;
            if (classname == "All")
            {
                return RedirectToAction("InActiveStudents", "Reports");
            }
            int classid = 1;
            DB37Entities db = new DB37Entities();
            classid = db.Classtbls.Where(t => t.Section == classname).FirstOrDefault().Id;

            //CrMVCApp.Models.Customer c;
            var c = db.ShowClassWiseInActiveStudents(classid).ToList();

            InActiveStudentsReport rpt = new InActiveStudentsReport();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult ClassWiseStudentTotalMarks()
        {
            string classanme = ReportsController.classname;
            DB37Entities db = new DB37Entities();
            if (classname == "All")
            {
                var c2 = db.TotalMarksOfAllClasses().ToList();

                TotalMarksOfClass rpt2 = new TotalMarksOfClass();
                rpt2.Load();
                rpt2.SetDataSource(c2);
                Stream s2 = rpt2.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                return File(s2, "application/pdf");
            }
            int classid = 1;
            
            classid = db.Classtbls.Where(t => t.Section == classname).FirstOrDefault().Id;

            //CrMVCApp.Models.Customer c;
            var c = db.TotalMarksOfEachClass(classid).ToList();

            TotalMarksOfClass rpt = new TotalMarksOfClass();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }

        public ActionResult AllEvents()
        {
            DB37Entities db = new DB37Entities();
            //CrMVCApp.Models.Customer c;
            var c = db.Show_Events.ToList();

            EventsReport rpt = new EventsReport();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
        public ActionResult AdminReportsOnly()
        {
            DB37Entities db = new DB37Entities();
            List<string> classlist = new List<string>();

            List<string> termList = new List<string>();
            foreach (Termstbl ttbl in db.Termstbls)
            {
                termList.Add(ttbl.Name);
            }
            ViewBag.Options1 = termList;

            string temp;
            classlist.Add("All");
            foreach (Classtbl c in db.Classtbls)
            {
                temp = c.Section;
                classlist.Add(temp);
            }

            ViewBag.Options = classlist;
            return View();
        }

        public JsonResult UpdateSubjects(string classname)
        {
            DB37Entities db = new DB37Entities();
            int classId = db.Classtbls.Where(t => t.Section == classname).FirstOrDefault().Id;
            if (!db.ClassSubjecttbls.Any(t => t.ClassId == classId))
            {
                List<string> subjectListEmpty = new List<string>();
                return Json(subjectListEmpty, JsonRequestBehavior.AllowGet);
            }


            db.Configuration.ProxyCreationEnabled = false;
            //List<ClassTbl> ClassList = db.ClassTbls.Where(x => x.School_Id == schoolId).ToList();
            List<string> subjectList = new List<string>();
            foreach (ClassSubjecttbl c in db.ClassSubjecttbls)
            {
                if (c.ClassId == classId)
                {
                    subjectList.Add(db.Subjectstbls.Where(t => t.Id == c.SubjectId).FirstOrDefault().Name);
                }
            }
            return Json(subjectList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult UpdateStudents(string classname)
        {
            DB37Entities db = new DB37Entities();
            int classId = db.Classtbls.Where(t => t.Section == classname).FirstOrDefault().Id;
            if (!db.StudentClasstbls.Any(t => t.ClassId == classId))
            {
                List<string> studentListEmpty = new List<string>();
                return Json(studentListEmpty, JsonRequestBehavior.AllowGet);
            }


            db.Configuration.ProxyCreationEnabled = false;
            //List<ClassTbl> ClassList = db.ClassTbls.Where(x => x.School_Id == schoolId).ToList();
            List<string> studentList = new List<string>();
            foreach (StudentClasstbl c in db.StudentClasstbls)
            {
                if (c.ClassId == classId)
                {
                    studentList.Add(db.Studentstbls.Where(t => t.Id == c.StudentId).FirstOrDefault().RegistrationNumber);
                }
            }
            return Json(studentList, JsonRequestBehavior.AllowGet);
        }

        // POST: Reports/Create
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

        // GET: Reports/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reports/Edit/5
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

        // GET: Reports/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reports/Delete/5
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
