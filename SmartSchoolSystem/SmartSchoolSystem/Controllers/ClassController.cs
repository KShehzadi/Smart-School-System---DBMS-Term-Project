using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SmartSchoolSystem.Models;
namespace SmartSchoolSystem.Controllers
{
    public class ClassController : Controller
    {
        
        // GET: Class
        public ActionResult Index()
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            ClassesViewModels.ClassesList.Clear();
            DB37Entities db = new DB37Entities();
            foreach(Classtbl c in db.Classtbls)
            {
                ClassesViewModels a = new ClassesViewModels();
                a.Id = c.Id;
                a.Name = c.Section;
                ClassesViewModels.ClassesList.Add(a);
            }
            return View(ClassesViewModels.ClassesList);
        }

        // GET: Class/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Class/Create
        public ActionResult Create()
        {
            if(HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Class/Create
        [HttpPost]
        public ActionResult Create(ClassesViewModels collection)
        {
            try
            {

                if (HelperClass.account != "Admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                DB37Entities db = new DB37Entities();
                if(db.Classtbls.Any(t=>t.Section.Equals(collection.Name)))
                {
                    ViewBag.warn = "Class Already Exist";
                    return View();
                }

                Classtbl c = new Classtbl();
                c.Section = collection.Name;
                db.Classtbls.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index","Class");
            }
            catch
            {
                return View();
            }
        }

        // GET: Class/Edit/5
        public ActionResult Edit(int id)
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            DB37Entities db = new DB37Entities();
            return View(ClassesViewModels.ClassesList.Find(t=>t.Id.Equals(id)));
        }

        // POST: Class/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,ClassesViewModels collection)
        {
            try
            {
                // TODO: Add update logic here

                if (HelperClass.account != "Admin")
                {
                    return RedirectToAction("Index", "Home");
                }
                DB37Entities db = new DB37Entities();
                db.Classtbls.Find(id).Section = collection.Name;
                db.SaveChanges();
                return RedirectToAction("Index","Class");
            }
            catch
            {
                return View();
            }
        }

        // GET: Class/Delete/5
        public ActionResult Delete(int id)
        {

            if (HelperClass.account != "Admin")
            {
                return RedirectToAction("Index", "Home");
            }
            DB37Entities db = new DB37Entities();
            db.Classtbls.Remove(db.Classtbls.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index", "Class");
        }

        // POST: Class/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index","Class");
            }
            catch
            {
                return View();
            }
        }
    }
}
