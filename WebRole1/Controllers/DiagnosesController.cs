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
    public class DiagnosesController : Controller
    {
        private CloudMedContext db = new CloudMedContext();

        // GET: Diagnoses
        public ActionResult Index()
        {
            var diagnoses = db.Diagnoses.Include(d => d.Consultation);
            return View(diagnoses.ToList());
        }

        // GET: Diagnoses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.Diagnoses.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        // GET: Diagnoses/Create
        public ActionResult Create()
        {
            ViewBag.ConsultationID = new SelectList(db.Consultations, "ConsultationID", "Status");
            return View();
        }

        // POST: Diagnoses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiagnosisID,Illness,Description,ConsultationID")] Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                db.Diagnoses.Add(diagnosis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ConsultationID = new SelectList(db.Consultations, "ConsultationID", "Status", diagnosis.ConsultationID);
            return View(diagnosis);
        }

        // GET: Diagnoses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.Diagnoses.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            ViewBag.ConsultationID = new SelectList(db.Consultations, "ConsultationID", "Status", diagnosis.ConsultationID);
            return View(diagnosis);
        }

        // POST: Diagnoses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiagnosisID,Illness,Description,ConsultationID")] Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnosis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ConsultationID = new SelectList(db.Consultations, "ConsultationID", "Status", diagnosis.ConsultationID);
            return View(diagnosis);
        }

        // GET: Diagnoses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.Diagnoses.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        // POST: Diagnoses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diagnosis diagnosis = db.Diagnoses.Find(id);
            db.Diagnoses.Remove(diagnosis);
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
