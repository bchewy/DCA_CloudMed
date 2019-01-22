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
    public class MedicalEquipmentsController : Controller
    {
        private CloudMedContext db = new CloudMedContext();

        // GET: MedicalEquipments
        public ActionResult Index()
        {
            var medicalEquipment = db.MedicalEquipment.Include(m => m.Diagnosis);
            return View(medicalEquipment.ToList());
        }

        // GET: MedicalEquipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalEquipment medicalEquipment = db.MedicalEquipment.Find(id);
            if (medicalEquipment == null)
            {
                return HttpNotFound();
            }
            return View(medicalEquipment);
        }

        // GET: MedicalEquipments/Create
        public ActionResult Create()
        {
            ViewBag.DiagnosisID = new SelectList(db.Diagnoses, "DiagnosisID", "Illness");
            return View();
        }

        // POST: MedicalEquipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipmentID,Name,Brand,SerialNumber,Status,SoftwareVersion,Warranty,PurchaseDate,LastMaintenance,DiagnosisID")] MedicalEquipment medicalEquipment)
        {
            if (ModelState.IsValid)
            {
                db.MedicalEquipment.Add(medicalEquipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DiagnosisID = new SelectList(db.Diagnoses, "DiagnosisID", "Illness", medicalEquipment.DiagnosisID);
            return View(medicalEquipment);
        }

        // GET: MedicalEquipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalEquipment medicalEquipment = db.MedicalEquipment.Find(id);
            if (medicalEquipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiagnosisID = new SelectList(db.Diagnoses, "DiagnosisID", "Illness", medicalEquipment.DiagnosisID);
            return View(medicalEquipment);
        }

        // POST: MedicalEquipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipmentID,Name,Brand,SerialNumber,Status,SoftwareVersion,Warranty,PurchaseDate,LastMaintenance,DiagnosisID")] MedicalEquipment medicalEquipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalEquipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiagnosisID = new SelectList(db.Diagnoses, "DiagnosisID", "Illness", medicalEquipment.DiagnosisID);
            return View(medicalEquipment);
        }

        // GET: MedicalEquipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalEquipment medicalEquipment = db.MedicalEquipment.Find(id);
            if (medicalEquipment == null)
            {
                return HttpNotFound();
            }
            return View(medicalEquipment);
        }

        // POST: MedicalEquipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalEquipment medicalEquipment = db.MedicalEquipment.Find(id);
            db.MedicalEquipment.Remove(medicalEquipment);
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
