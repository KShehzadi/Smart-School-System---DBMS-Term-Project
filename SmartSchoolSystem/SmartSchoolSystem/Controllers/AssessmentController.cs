using SmartSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSchoolSystem.Controllers
{
    public class AssessmentController : Controller
    {
        string selectedClass;
        string selectedSubject;
        string selectedTerm;
        
        
        // GET: Assessment
        public ActionResult Index()
        {
            return View();
        }

        // GET: Assessment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Assessment/Create
        public ActionResult Create()
        {
            DB37Entities db = new DB37Entities();

            List<string> termList = new List<string>();
            foreach (Termstbl ttbl in db.Termstbls)
            {
                termList.Add(ttbl.Name);
            }
            ViewBag.Options1 = termList;

            List<string> classList = new List<string>();
            foreach (Classtbl ctbl in db.Classtbls)
            {
                classList.Add(ctbl.Section);
            }
            ViewBag.Options = classList;

            List<string> subjectList = new List<string>();
            foreach (Subjectstbl stbl in db.Subjectstbls)
            {
                subjectList.Add(stbl.Name);
            }

            

            List<AssessmentViewModel> myList = new List<AssessmentViewModel>();
            AssessmentViewModel ass = new AssessmentViewModel();
            //ViewBag.myModel = myList;

            return View(myList);
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

        // POST: Assessment/Create
        [HttpPost]
        public ActionResult Create(FormCollection form, string sub, AssessmentViewModel obj)
        {
            try
            {
                
                
                DB37Entities db = new DB37Entities();

                List<string> termList = new List<string>();
                foreach (Termstbl ttbl in db.Termstbls)
                {
                    termList.Add(ttbl.Name);
                }
                ViewBag.Options1 = termList;

                List<string> classList = new List<string>();
                foreach (Classtbl ctbl in db.Classtbls)
                {
                    classList.Add(ctbl.Section);
                }
                ViewBag.Options = classList;

                List<string> subjectList = new List<string>();
                foreach (Subjectstbl stbl in db.Subjectstbls)
                {
                    subjectList.Add(stbl.Name);
                }
                if (AssessmentViewModel.flag == false)
                {
                    
                }

                
                // TODO: Add insert logic here
                if (sub == "show")
                {
                    AssessmentViewModel.totalMarks = Convert.ToInt32(form["total"]);

                    selectedClass = form["Classlist"].ToString();
                    if (form["Classlist"].ToString() == null)
                    {
                        ViewBag.warn = "Select subjects before clicking show";
                        return View();
                    }
                    AssessmentViewModel.classId = db.Classtbls.Where(t1 => t1.Section.Equals(selectedClass)).FirstOrDefault().Id;
                    if (form["subjectList"].ToString() == null)
                    {
                        ViewBag.warn = "Select subjects before clicking show";
                        return View();
                    }

                    selectedSubject = form["subjectList"].ToString();
                    AssessmentViewModel.subjectId = db.Subjectstbls.Where(t2 => t2.Name.Equals(selectedSubject)).FirstOrDefault().Id;

                    selectedTerm = form["Termlist"].ToString();
                    AssessmentViewModel.termId = db.Termstbls.Where(t4 => t4.Name.Equals(selectedTerm)).FirstOrDefault().Id;
                    AssessmentViewModel.assessmentList.Clear();
                    AssessmentViewModel.flag = true;

                    

                    foreach (StudentClasstbl sctbl in db.StudentClasstbls)
                    {
                        if (sctbl.ClassId == AssessmentViewModel.classId)
                        {
                            AssessmentViewModel assessment = new AssessmentViewModel();
                            assessment.studentId = sctbl.StudentId;
                            assessment.regNo = db.Studentstbls.Where(t3 => t3.Id.Equals(sctbl.StudentId)).FirstOrDefault().RegistrationNumber;
                            AssessmentViewModel.assessmentList.Add(assessment);
                        }
                    }
                    //ViewBag.myModel = AssessmentViewModel.assessmentList;
                    return View(AssessmentViewModel.assessmentList);
                }
                else
                {
                    string[] keys = form.AllKeys;
                    var added = form[keys[3]].Split(',');
                    var p = added[0];
                    

                    AssessmentViewModel a = new AssessmentViewModel();
                    int count = 0;
                    
                    foreach (AssessmentViewModel a1 in AssessmentViewModel.assessmentList)
                    {
                        if (db.Markingstbls.Any(t6 => t6.ClassId == AssessmentViewModel.classId && t6.SubjectId == AssessmentViewModel.subjectId && t6.StudentId == a1.studentId && t6.TermId == AssessmentViewModel.termId))
                        {
                            db.Markingstbls.Where(t7 => t7.ClassId == AssessmentViewModel.classId && t7.SubjectId == AssessmentViewModel.subjectId && t7.StudentId == a1.studentId && t7.TermId == AssessmentViewModel.termId).FirstOrDefault().ObtainedMarks = Convert.ToInt32(added[count]);
                        }
                        else
                        {
                            Markingstbl mtbl = new Markingstbl();
                            mtbl.ClassId = AssessmentViewModel.classId;
                            mtbl.SubjectId = AssessmentViewModel.subjectId;
                            mtbl.StudentId = a1.studentId;
                            mtbl.TotalMarks = AssessmentViewModel.totalMarks;
                            mtbl.ObtainedMarks = Convert.ToInt32(added[count]);
                            mtbl.TermId = AssessmentViewModel.termId;
                            db.Markingstbls.Add(mtbl);
                        }
                        count++;
                    }
                    db.SaveChanges();

                    return RedirectToAction("Create");
                }



            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

        // GET: Assessment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Assessment/Edit/5
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

        // GET: Assessment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Assessment/Delete/5
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
