using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CampaniasLito.Filters;
using CampaniasLito.Models;

namespace CampaniasLito.Controllers
{
    public class StudentsController : Controller
    {
        private CampaniasLitoContext db = new CampaniasLitoContext();

        // GET: Students
        [AuthorizeUser(idOperacion: 5)]
        public JsonResult GetStudentDetails()
        {
            try
            {
                List<Student> students = new List<Student>();
                students = db.Students.ToList();
                return Json(new { Result = "OK", Records = students }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        // GET: Students
        [AuthorizeUser(idOperacion: 5)]
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        [AuthorizeUser(idOperacion: 6)]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public JsonResult Create(Student Model)
        {
            try
            {
                Model.StudentId = Guid.NewGuid().ToString();

                db.Students.Add(Model);
                db.SaveChanges();
                return Json(new { Result = "OK", Records = Model }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        public JsonResult Edit(Student Model)
        {
            try
            {
                db.Entry(Model).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [AuthorizeUser(idOperacion: 4)]
        [HttpPost]
        public JsonResult Delete(String StudentID)
        {
            try
            {
                Student students = db.Students.Find(StudentID);
                db.Students.Remove(students);
                db.SaveChanges();
                return Json(new { Result = "OK" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
