using SmartSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSchoolSystem.Controllers
{
    public class SubjectController : Controller
    {
        // GET: Subject
        public ActionResult Index()
        {
            if(HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index2", "Subject");
        }
        
        public ActionResult Index2()
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            SubjectViewModel.subjectList.Clear();
            DB37Entities db = new DB37Entities();
            List<string> mylist = new List<string>();
            foreach (Classtbl c in db.Classtbls)
            {
                mylist.Add(c.Section);
            }
            ViewBag.Options = mylist;
            ViewBag.Classname = "";
            SubjectViewModel.subjectList.Clear();

            foreach (Subjectstbl s in db.Subjectstbls)
            {
                SubjectViewModel mysubject = new SubjectViewModel();
                mysubject.id = s.Id;
                mysubject.name = s.Name;
                SubjectViewModel.subjectList.Add(mysubject);
            }

            return View(SubjectViewModel.subjectList);

        }
        [HttpPost]
        public ActionResult Index2(FormCollection form)
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            SubjectViewModel.subjectList.Clear();
            DB37Entities db = new DB37Entities();
            List<string> mylist = new List<string>();
            foreach (Classtbl c in db.Classtbls)
            {
                mylist.Add(c.Section);
            }
            ViewBag.Options = mylist;
            
            string classname = form["Classlist"].ToString();
            ViewBag.Classname = classname;
            int classid = db.Classtbls.Where(t => t.Section.Equals(classname)).FirstOrDefault().Id;
            foreach (ClassSubjecttbl cs in db.ClassSubjecttbls)
            {
                if (cs.ClassId == classid)
                {
                    SubjectViewModel s = new SubjectViewModel();
                    s.id = db.Subjectstbls.Where(t => t.Id == cs.SubjectId).FirstOrDefault().Id;
                    s.name = db.Subjectstbls.Where(t => t.Id == cs.SubjectId).FirstOrDefault().Name;

                    SubjectViewModel.subjectList.Add(s);
                }
            }

            return View(SubjectViewModel.subjectList);

        }

        // GET: Subject/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Subject/Create
        public ActionResult Create()
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            DB37Entities db = new DB37Entities();
            List<string> mylist = new List<string>();
            foreach(Classtbl c in db.Classtbls)
            {
                mylist.Add(c.Section);
            }
            ViewBag.Options = mylist;
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        public ActionResult Create(SubjectViewModel sub, FormCollection form)
        {
            try
            {

                if (HelperClass.account != "Admin")
                {
                    return RedirectToAction("Index", "Home");
                }

                DB37Entities db = new DB37Entities();
                List<string> mylist = new List<string>();
                foreach (Classtbl c in db.Classtbls)
                {
                    mylist.Add(c.Section);
                }
                ViewBag.Options = mylist;
                string classname = form["Classlist"].ToString();
                
                int classid = db.Classtbls.Where(t => t.Section.Equals(classname)).FirstOrDefault().Id;
                
                if(db.Subjectstbls.Any(t => t.Name == sub.name) == false)
                {
                    Subjectstbl s = new Subjectstbl();
                    s.Name = sub.name;
                    db.Subjectstbls.Add(s);
                    db.SaveChanges();
                }
                DB37Entities db2 = new DB37Entities();
                int subjectid = db2.Subjectstbls.Where(t => t.Name == sub.name).FirstOrDefault().Id;
                if (db2.ClassSubjecttbls.Any(t=> t.ClassId == classid && t.SubjectId == subjectid))
                {
                    ViewBag.warn = "Class Already has this Subject";
                    return View();
                }
                
                ClassSubjecttbl cs = new ClassSubjecttbl();
                cs.SubjectId = subjectid;
                cs.ClassId = classid;
                db2.ClassSubjecttbls.Add(cs);
                db2.SaveChanges();
                return RedirectToAction("Index", "Subject");
            }
            catch
            {
                return View();
            }
        }

        // GET: Subject/Edit/5
        public ActionResult Edit(int id)
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }

            SubjectViewModel.sampleSubject = SubjectViewModel.subjectList.Where(t => t.id == id).FirstOrDefault();
            return View(SubjectViewModel.sampleSubject);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SubjectViewModel collection)
        {
            try
            {

                if (HelperClass.account != "Admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                DB37Entities db = new DB37Entities();
                if(db.Subjectstbls.Any(t=> t.Name == collection.name))
                {
                    ViewBag.warn = "Subject with this name already Exist";
                    return View();
                }
                // TODO: Add update logic here
                db.Subjectstbls.Where(t => t.Id == id).FirstOrDefault().Name = collection.name;
                db.SaveChanges();
                return RedirectToAction("Index", "Subject");
            }
            catch
            {
                return View();
            }
        }

        // GET: Subject/Delete/5
        public ActionResult Delete(int id)
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            DB37Entities db = new DB37Entities();
            db.Subjectstbls.Remove(db.Subjectstbls.Where(t => t.Id == id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction("Index", "Subject");
        }

        // POST: Subject/Delete/5
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
