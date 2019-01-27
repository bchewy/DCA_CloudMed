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
            var medicalRecord = db.MedicalRecord.Include(m => m.Diagnosis);
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
            return View(medicalRecord);
        }

        // GET: MedicalRecords/Create
        public ActionResult Create()
        {
            ViewBag.DiagnosisID = new SelectList(db.Diagnoses, "DiagnosisID", "Illness");
            return View();
        }

        // POST: MedicalRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordID,Description,DocURL,DiagnosisID")] MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                db.MedicalRecord.Add(medicalRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DiagnosisID = new SelectList(db.Diagnoses, "DiagnosisID", "Illness", medicalRecord.DiagnosisID);
            return View(medicalRecord);
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
            ViewBag.DiagnosisID = new SelectList(db.Diagnoses, "DiagnosisID", "Illness", medicalRecord.DiagnosisID);
            return View(medicalRecord);
        }

        // POST: MedicalRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordID,Description,DocURL,DiagnosisID")] MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiagnosisID = new SelectList(db.Diagnoses, "DiagnosisID", "Illness", medicalRecord.DiagnosisID);
            return View(medicalRecord);
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
