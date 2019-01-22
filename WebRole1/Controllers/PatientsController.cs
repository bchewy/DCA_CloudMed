﻿using System;
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
    public class PatientsController : Controller
    {
        private CloudMedContext db = new CloudMedContext();

        // GET: Patients
        public ActionResult Index(string so)
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(so) ? "Name" : "";
            ViewBag.DateSortParam = so == "DOB" ? "DOB" : "DOB";
            var patients = from p in db.Patients select p;
            switch (so)
            {
                case "Name":
                    patients = patients.OrderByDescending(p => p.Name);
                    break;
                case "DOB":
                    patients = patients.OrderBy(p => p.DoB);
                    break;
                default:
                    patients = patients.OrderBy(s => s.ICNo);
                    break;
            }
            return View(patients.ToList());
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientID,Address,DoB,PersonID,ICNo,Name,Citizenship,EmailAddr,PatientImageURL")] PatientViewModel PatientViewModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Images") + file.FileName);
                }
                var patient = new Patient()
                {
                    ICNo = PatientViewModel.ICNo,
                    Name = PatientViewModel.Name,
                    Gender = PatientViewModel.Gender,
                    Citizenship = PatientViewModel.Citizenship,
                    EmailAddr = PatientViewModel.EmailAddr,
                    Address = PatientViewModel.Address,
                    DoB = PatientViewModel.DoB,
                    PatientImageURL = PatientViewModel.PatientImageURL
                };
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(PatientViewModel);
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientID,Address,DoB,PersonID,ICNo,Name,Citizenship,EmailAddr")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            db.Patients.Remove(patient);
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
