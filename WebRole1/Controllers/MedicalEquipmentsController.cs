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
        [Authorize(Roles = "Administrator")]
        public ViewResult Index(string so, string searchString)
        {
   
            
            ViewBag.NameSortParam = String.IsNullOrEmpty(so) ? "Name" : "";
            var medicalequipment = from me in db.MedicalEquipment select me;
            if (!String.IsNullOrEmpty(searchString))
            {
                medicalequipment = medicalequipment.Where(me => me.Name.Contains(searchString));
            }
            switch (so)
            {
                case "Name":
                    medicalequipment = medicalequipment.OrderByDescending(me => me.Name);
                    break;
                default:
                    medicalequipment = medicalequipment.OrderBy(me => me.Brand);
                    break;

            }
            //return View(medicalequipment.ToList());
            return View(db.MedicalEquipment.ToList());
        }




        // GET: MedicalEquipments/Details/5
        [Authorize(Roles = "Administrator")]
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

            MedicalEquipmentViewModel MedicalEquipmentViewModel = new MedicalEquipmentViewModel()
            {
                EquipmentID = medicalEquipment.EquipmentID,
                Name = medicalEquipment.Name,
                Brand = medicalEquipment.Brand,
                SerialNumber = medicalEquipment.SerialNumber,
                Status = medicalEquipment.Status,
                SoftwareVersion = medicalEquipment.SoftwareVersion,
                PurchaseDate = medicalEquipment.PurchaseDate,
                Warranty = medicalEquipment.Warranty,
                LastMaintenance = medicalEquipment.LastMaintenance
                
            };
            return View(MedicalEquipmentViewModel);
        }

        // GET: MedicalEquipments/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: MedicalEquipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquipmentID,Name,Brand,SerialNumber,Status,SoftwareVersion,Warranty,PurchaseDate,LastMaintenance")] MedicalEquipmentViewModel medicalEquipmentViewModel)

        {
            if (ModelState.IsValid)
            {
                var medicalequipment = new MedicalEquipment()
                {
                    Name = medicalEquipmentViewModel.Name,
                    Brand = medicalEquipmentViewModel.Brand,
                    SerialNumber = medicalEquipmentViewModel.SerialNumber,
                    Status = medicalEquipmentViewModel.Status,
                    SoftwareVersion = medicalEquipmentViewModel.SoftwareVersion,
                    Warranty = medicalEquipmentViewModel.Warranty,
                    PurchaseDate = medicalEquipmentViewModel.PurchaseDate,
                    LastMaintenance = medicalEquipmentViewModel.LastMaintenance


                };

                db.MedicalEquipment.Add(medicalequipment);
                db.SaveChanges();

                int newMedEquip = medicalequipment.EquipmentID;
       
                return RedirectToAction("Index");


            }

            return View(medicalEquipmentViewModel);
        }



        // GET: MedicalEquipments/Edit/5
        [Authorize(Roles = "Administrator")]
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

            MedicalEquipmentViewModel MedicalEquimentViewModel = new MedicalEquipmentViewModel()
            {
                EquipmentID = medicalEquipment.EquipmentID,
                Name=medicalEquipment.Name,
                Brand = medicalEquipment.Brand,
                SerialNumber = medicalEquipment.SerialNumber,
                Status = medicalEquipment.Status,
                SoftwareVersion = medicalEquipment.SoftwareVersion,
                Warranty = medicalEquipment.Warranty,
                PurchaseDate = medicalEquipment.PurchaseDate,
                LastMaintenance = medicalEquipment.LastMaintenance

            };

            return View(MedicalEquimentViewModel);

            
        }

        // POST: MedicalEquipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquipmentID,Name,Brand,SerialNumber,Status,SoftwareVersion,Warranty,PurchaseDate,LastMaintenance")] MedicalEquipmentViewModel MedicalEquipmentViewModel)
        {
            if (ModelState.IsValid)
            {
                var medicalequipment = db.MedicalEquipment.Find(MedicalEquipmentViewModel.EquipmentID);

                medicalequipment.Name = MedicalEquipmentViewModel.Name;
                medicalequipment.Brand = MedicalEquipmentViewModel.Brand;
                medicalequipment.SerialNumber = MedicalEquipmentViewModel.SerialNumber;
                medicalequipment.Status = MedicalEquipmentViewModel.Status;
                medicalequipment.SoftwareVersion = MedicalEquipmentViewModel.SoftwareVersion;
                medicalequipment.Warranty = MedicalEquipmentViewModel.Warranty;
                medicalequipment.PurchaseDate = MedicalEquipmentViewModel.PurchaseDate;
                medicalequipment.LastMaintenance = MedicalEquipmentViewModel.LastMaintenance;


                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(MedicalEquipmentViewModel);
        }



        // GET: MedicalEquipments/Delete/5
       [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MedicalEquipment medicalEquipment = db.MedicalEquipment.Find(id);

            MedicalEquipment medicalEquipments = db.MedicalEquipment.Remove(medicalEquipment);// THIS MIGHT HAVE ERROR THE CODE PROBABLY IS WRONG
           // db.MedicalEquipment.Remove(medicalEquipment);
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
