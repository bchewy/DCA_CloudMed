using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebRole1.DAL;
using WebRole1.Models;

namespace WebRole1.Controllers
{
    public class CoursesController : Controller
    {
        private CloudMedContext db = new CloudMedContext();

        // GET: Courses
        public ActionResult Index()
        {
            List<Course> courses = db.Courses.ToList();
            List<CourseViewModel> courseList = new List<CourseViewModel>();
            foreach (Course crse in courses)
            {
                if (crse is Seminar)
                {
                    Seminar c = (Seminar)crse;
                    CourseViewModel cvm = new CourseViewModel
                    {
                        CourseID = c.CourseID,
                        Title = c.Title,
                        Description = c.Description,
                        Category = c.Category,
                        CourseFee = c.CourseFee,
                        DoctorID = c.DoctorID,
                        Duration = c.Duration,
                        Date = c.Date,
                        Time = c.Time,
                        Capacity = c.Capacity,
                        Venue = c.Venue,
                        CourseType = CourseViewModel.CourTypes.Seminar
                    };
                    courseList.Add(cvm);
                }
                else
                {
                    Webinar c = (Webinar)crse;
                    CourseViewModel cvm = new CourseViewModel
                    {
                        CourseID = c.CourseID,
                        Title = c.Title,
                        Description = c.Description,
                        Category = c.Category,
                        CourseFee = c.CourseFee,
                        DoctorID = c.DoctorID,
                        URL = c.URL,
                        DateReleased = c.DateReleased,
                        CourseType = CourseViewModel.CourTypes.Webinar
                    };
                    courseList.Add(cvm);
                }
            }
            return View(courseList);
            

            //var courses = db.Courses.Include(c => c.Doctor);
            //return View(courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            if (course is Seminar)
            {
                Seminar c = (Seminar)course;
                CourseViewModel cvm = new CourseViewModel
                {
                    CourseID = c.CourseID,
                    Title = c.Title,
                    Description = c.Description,
                    Category = c.Category,
                    CourseFee = c.CourseFee,
                    DoctorID = c.DoctorID,
                    Duration = c.Duration,
                    Date = c.Date,
                    Time = c.Time,
                    Capacity = c.Capacity,
                    Venue = c.Venue,
                    CourseType = CourseViewModel.CourTypes.Seminar
                };
                return View(cvm);
            }
            else
            {
                Webinar c = (Webinar)course;
                CourseViewModel cvm = new CourseViewModel
                {
                    CourseID = c.CourseID,
                    Title = c.Title,
                    Description = c.Description,
                    Category = c.Category,
                    CourseFee = c.CourseFee,
                    DoctorID = c.DoctorID,
                    URL = c.URL,
                    DateReleased = c.DateReleased,
                    CourseType = CourseViewModel.CourTypes.Seminar
                };
                return View(cvm);
            }

            //return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "Specialty");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Description,Category,CourseFee,DoctorID,CourseType,Duration,Date,Time,Capacity,Venue,URL,DateReleased")] CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                if (courseViewModel.CourseType.ToString() == "Seminar")
                {
                    var seminar = new Seminar
                    {
                        CourseID = courseViewModel.CourseID,
                        Title = courseViewModel.Title,
                        Description = courseViewModel.Description,
                        Category = courseViewModel.Category,
                        CourseFee = courseViewModel.CourseFee,
                        DoctorID = courseViewModel.DoctorID,
                        Duration = courseViewModel.Duration,
                        Date = courseViewModel.Date,
                        Time = courseViewModel.Time,
                        Capacity = courseViewModel.Capacity,
                        Venue = courseViewModel.Venue
                    };
                    db.Courses.Add(seminar);
                    db.SaveChanges();
                }
                else
                {
                    var webinar = new Webinar
                    {
                        CourseID = courseViewModel.CourseID,
                        Title = courseViewModel.Title,
                        Description = courseViewModel.Description,
                        Category = courseViewModel.Category,
                        CourseFee = courseViewModel.CourseFee,
                        DoctorID = courseViewModel.DoctorID,
                        URL = courseViewModel.URL,
                        DateReleased = courseViewModel.DateReleased
                    };
                    db.Courses.Add(webinar);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "BranchID", "Specialty",
            courseViewModel.DoctorID);
            return View(courseViewModel);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            if (course is Seminar)
            {
                Seminar c = (Seminar)course;
                CourseViewModel cvm = new CourseViewModel
                {
                    CourseID = c.CourseID,
                    Title = c.Title,
                    Description = c.Description,
                    Category = c.Category,
                    CourseFee = c.CourseFee,
                    DoctorID = c.DoctorID,
                    Duration = c.Duration,
                    Date = c.Date,
                    Time = c.Time,
                    Capacity = c.Capacity,
                    Venue = c.Venue,
                    CourseType = CourseViewModel.CourTypes.Seminar
                };
                ViewBag.Courses = new SelectList(db.Courses, "DoctorID", "Specialty", c.DoctorID);
                return View(cvm);
            }
            else
            {
                Webinar c = (Webinar)course;
                CourseViewModel cvm = new CourseViewModel
                {
                    CourseID = c.CourseID,
                    Title = c.Title,
                    Description = c.Description,
                    Category = c.Category,
                    CourseFee = c.CourseFee,
                    DoctorID = c.DoctorID,
                    URL = c.URL,
                    DateReleased = c.DateReleased,
                    CourseType = CourseViewModel.CourTypes.Webinar
                };
                ViewBag.Courses = new SelectList(db.Courses, "DoctorID", "Specialty", c.DoctorID);
                return View(cvm);
            }
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID,Title,Description,Category,CourseFee,DoctorID,Duration,Date,Time,Capacity,Venue,URL,DateReleased")] CourseViewModel courseViewModel)
        {
            if (ModelState.IsValid)
            {
                var course = db.Courses.Find(courseViewModel.CourseID);
                var doctor = db.Doctors.Find(courseViewModel.DoctorID);

                if (course is Seminar)
                {
                    Seminar seminar = (Seminar)course;
                    seminar.CourseID = courseViewModel.CourseID;
                    seminar.Title = courseViewModel.Title;
                    seminar.Description = courseViewModel.Description;
                    seminar.Category = courseViewModel.Category;
                    seminar.CourseFee = courseViewModel.CourseFee;
                    seminar.DoctorID = courseViewModel.DoctorID;
                    seminar.Duration = courseViewModel.Duration;
                    seminar.Date = courseViewModel.Date;
                    seminar.Time = courseViewModel.Time;
                    seminar.Capacity = courseViewModel.Capacity;
                    seminar.Venue = courseViewModel.Venue;

                    seminar.Doctor = doctor;
                    db.Entry(seminar).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                else if (course is Webinar)
                {
                    Webinar webinar = (Webinar)course;
                    webinar.CourseID = courseViewModel.CourseID;
                    webinar.Title = courseViewModel.Title;
                    webinar.Description = courseViewModel.Description;
                    webinar.Category = courseViewModel.Category;
                    webinar.CourseFee = courseViewModel.CourseFee;
                    webinar.DoctorID = courseViewModel.DoctorID;
                    webinar.URL = courseViewModel.URL;
                    webinar.DateReleased = courseViewModel.DateReleased;

                    webinar.Doctor = doctor;
                    db.Entry(webinar).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID",
            "Specialty", courseViewModel.DoctorID);
            return View(courseViewModel);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
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
