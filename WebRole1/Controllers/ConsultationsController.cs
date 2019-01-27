using Microsoft.AspNet.Identity;
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
           
            var userID = User.Identity.GetUserId();
            var consultations = db.Consultations.Include(c => c.Doctor).Include(c => c.Patient);
           

            ViewBag.QueueSortParam = String.IsNullOrEmpty(Queueso) ? "QueueNo" : "";
            ViewBag.QueueSortParam = Queueso == "QueueNo" ? "QueueNo" : "QueueNodesc";


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
            List<ConsultationViewModel> consultVMList = new List<ConsultationViewModel>();
            foreach (Consultation consult in db.Consultations)
            {
                ConsultationViewModel consultVM = new ConsultationViewModel();
                consultVM.ConsultationType = consult.ConsultationType;
                consultVM.QueueNo = consult.QueueNo;
                consultVM.Status = consult.Status;
                consultVM.TimeStamp = consult.TimeStamp;
                consultVM.ConsultationID = consult.ConsultationID;
                consultVM.patient = consult.Patient;
                consultVM.doctor = consult.Doctor;
                consultVM.QueueColor = "green";
                DateTime thisDay = DateTime.Today;
                if (consult.TimeStamp >= thisDay)
                {
                    consultVM.DateColor = "blue";
                }
                else
                {
                    consultVM.DateColor = "brown";
                }
                if (consult.QueueNo > 4)
                {
                    consultVM.QueueColor = "orange";
                }
                else if (consult.QueueNo > 9)
                {
                    consultVM.QueueColor = "red";
                }
                switch (consult.ConsultationType)
                {
                    case "Urgent":
                        consultVM.typeColor = "red";
                        break;
                    case "Normal":
                        consultVM.typeColor = "orange";
                        break;
                    case "When Available":
                        consultVM.typeColor = "green";
                        break;
                    default:
                        break;
                }
                consultVMList.Add(consultVM);
            }
            
            return View("Index", consultVMList);
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
        [Authorize(Roles = "Administrator, Patient")]
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
        [Authorize(Roles = "Administrator, Patient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsultationID,TimeStamp,Status,ConsultationType,PatientID,DoctorID")] Consultation consultation)
        {
            List<DateTime> dateQuery = (from thing in db.Consultations where thing.DoctorID == consultation.DoctorID select thing.TimeStamp).ToList();
            foreach (DateTime present in dateQuery)
            {
                if (present == consultation.TimeStamp)
                {
                    ModelState.AddModelError("Date Error", "The entered Timestamp is taken for this doctor");
                    return JavaScript("window.alert('The Specified timing is taken already.');");
                }
            }
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
        [Authorize(Roles = "Administrator, Patient")]
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
        [Authorize(Roles = "Administrator, Patient")]
        public ActionResult Edit([Bind(Include = "ConsultationID,QueueNo,TimeStamp,Status,ConsultationType,PatientID,DoctorID")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultation).State = EntityState.Modified;
                ConsultationViewModel consultationViewModel = new ConsultationViewModel()
                {
                    ConsultationType = consultation.ConsultationType,
                    Status = consultation.Status,
                    TimeStamp = consultation.TimeStamp,
                    patient = consultation.Patient,
                    doctor = consultation.Doctor
                };
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "Specialty", consultation.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientImageURL", consultation.PatientID);
            return View(consultation);
        }

        // GET: Consultations/Delete/5
        [Authorize(Roles = "Administrator, Patient")]
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
        [Authorize(Roles = "Administrator, Patient")]
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
