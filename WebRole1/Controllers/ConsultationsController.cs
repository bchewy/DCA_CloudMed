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
    public class ConsultationsController : Controller
    {
        private CloudMedContext db = new CloudMedContext();

        // GET: Consultations
        public ActionResult Index(string Queueso, string docNameSort, string patNameSort, string searchString)
        {
            ViewBag.QueueSortParam = String.IsNullOrEmpty(Queueso) ? "QueueNo" : "";
            ViewBag.QueueSortParam = Queueso == "QueueNo" ? "QueueNo" : "QueueNodesc";
            

            var consultations = db.Consultations.Include(c => c.Doctor).Include(c => c.Patient);
            if (!String.IsNullOrEmpty(searchString))
            {
                consultations = consultations.Where(p => p.Doctor.Name.Contains(searchString));
                
            }
            switch (Queueso)
            {
                case "QueueNo":
                    consultations = consultations.OrderBy(o => o.QueueNo);
                    break;
                case "QueueNodesc":
                    consultations = consultations.OrderByDescending(o => o.QueueNo);
                    break;
                default:
                    break;
            }

            return View(consultations.ToList());
        }

        // GET: Consultations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.Consultations.Find(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }

            return View(consultation);
        }

        // GET: Consultations/Create
        public ActionResult Create()
        {

            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "Name");
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name");

            return View();
        }

        // POST: Consultations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsultationID,TimeStamp,Status,ConsultationType,PatientID,DoctorID")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                var query = db.Consultations.Count();
                Consultation consultation1 = new Consultation()
                {
                    QueueNo = query+1,
                    ConsultationType = consultation.ConsultationType,
                    Status = consultation.Status,
                    TimeStamp = consultation.TimeStamp,
                    PatientID = consultation.PatientID,
                    DoctorID = consultation.DoctorID
                };
                db.Consultations.Add(consultation1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "Name", consultation.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "Name", consultation.PatientID);
            return View(consultation);
        }

        // GET: Consultations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.Consultations.Find(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "Specialty", consultation.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientImageURL", consultation.PatientID);
            return View(consultation);
        }

        // POST: Consultations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConsultationID,QueueNo,TimeStamp,Status,ConsultationType,PatientID,DoctorID")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "Specialty", consultation.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientImageURL", consultation.PatientID);
            return View(consultation);
        }

        // GET: Consultations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultation consultation = db.Consultations.Find(id);
            if (consultation == null)
            {
                return HttpNotFound();
            }
            return View(consultation);
        }

        // POST: Consultations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consultation consultation = db.Consultations.Find(id);
            db.Consultations.Remove(consultation);
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
