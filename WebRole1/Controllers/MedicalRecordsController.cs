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
    public class MedicalRecordsController : Controller
    {
        private CloudMedContext db = new CloudMedContext();

        // GET: MedicalRecords
        public ActionResult Index()
        {
            var medicalRecord = db.MedicalRecord.Include(m => m.Consultation);
            return View(medicalRecord.ToList());
        }

        // GET: MedicalRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalRecord medicalRecord = db.MedicalRecord.Find(id);
            if (medicalRecord == null)
            {
                return HttpNotFound();
            }
            MedicalRecordsViewModel medicalRecordsViewModel = new MedicalRecordsViewModel()
            {
                Description = medicalRecord.Description,
                DocURL = medicalRecord.DocURL,
                Illness = medicalRecord.Illness,
                ConsultationID = medicalRecord.ConsultationID
            };
            return View(medicalRecordsViewModel);
        }

        // GET: MedicalRecords/Create
        public ActionResult Create()
        {
            ViewBag.ConsultationID = new SelectList(db.Consultations, "ConsultationID", "ConsultationID");
            return View();
        }

        // POST: MedicalRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordID,Description,DocURL,Illness,ConsultationID")] MedicalRecordsViewModel medicalRecordViewModel)
        {
            if (ModelState.IsValid)
            {
                var medicalRecord = new MedicalRecord()
                {
                    Description = medicalRecordViewModel.Description,
                    DocURL = medicalRecordViewModel.DocURL,
                    Illness = medicalRecordViewModel.Illness,
                    ConsultationID = medicalRecordViewModel.ConsultationID
                };
                db.MedicalRecord.Add(medicalRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConsultationID = new SelectList(db.Consultations, "ConsultationID", "ConsultationID", medicalRecordViewModel.ConsultationID);
            return View(medicalRecordViewModel);
        }

        // GET: MedicalRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalRecord medicalRecord = db.MedicalRecord.Find(id);
            if (medicalRecord == null)
            {
                return HttpNotFound();
            }
            MedicalRecordsViewModel medicalRecordsViewModel = new MedicalRecordsViewModel()
            {
                Description = medicalRecord.Description,
                DocURL = medicalRecord.DocURL,
                Illness = medicalRecord.Illness,
                ConsultationID = medicalRecord.ConsultationID

            };
            ViewBag.ConsultationID = new SelectList(db.Consultations, "ConsultationID", "ConsultationID", medicalRecord.ConsultationID);
            return View(medicalRecordsViewModel);
        }

        // POST: MedicalRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordID,Description,DocURL,Illness,ConsultationID")] MedicalRecordsViewModel medicalRecordViewModel)
        {
            if (ModelState.IsValid)
            {
                var medicalrecord = db.MedicalRecord.Find(medicalRecordViewModel.ConsultationID);
                medicalrecord.Description = medicalRecordViewModel.Description;
                medicalrecord.DocURL = medicalRecordViewModel.DocURL;
                medicalrecord.Illness = medicalRecordViewModel.Illness;
                medicalrecord.ConsultationID = medicalRecordViewModel.ConsultationID;
                db.Entry(medicalRecordViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConsultationID = new SelectList(db.Consultations, "ConsultationID", "ConsultationID", medicalRecordViewModel.ConsultationID);
            return View(medicalRecordViewModel);
        }

        // GET: MedicalRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalRecord medicalRecord = db.MedicalRecord.Find(id);
            if (medicalRecord == null)
            {
                return HttpNotFound();
            }
            return View(medicalRecord);
        }

        // POST: MedicalRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalRecord medicalRecord = db.MedicalRecord.Find(id);
            db.MedicalRecord.Remove(medicalRecord);
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
