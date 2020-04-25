using SmartSchoolSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSchoolSystem.Controllers
{
    public class StudentMarkingController : Controller
    {
        // GET: StudentMarking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mark()
        {
            StudentMarkingViewModel.studentsList.Clear();

            StudentMarkingViewModel marking = new StudentMarkingViewModel();
            marking.id = 1;
            marking.studentId = 1;
            marking.studentName = "Abc";
            marking.studentRegNo = "2016-CS-105";

            StudentMarkingViewModel.studentsList.Add(marking);

            StudentMarkingViewModel marking2 = new StudentMarkingViewModel();
            marking2.id = 2;
            marking2.studentId = 2;
            marking2.studentName = "Def";
            marking2.studentRegNo = "2016-CS-106";

            StudentMarkingViewModel.studentsList.Add(marking2);


            return View(StudentMarkingViewModel.studentsList);
        }

        // GET: StudentMarking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentMarking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentMarking/Create
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

        // GET: StudentMarking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StudentMarking/Edit/5
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

        // GET: StudentMarking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentMarking/Delete/5
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
