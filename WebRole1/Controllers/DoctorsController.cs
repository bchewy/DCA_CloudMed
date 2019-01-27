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
using Microsoft.WindowsAzure.Storage.Table.DataServices;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Queue.Protocol;
using System.Threading;

namespace WebRole1.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class DoctorsController : Controller
    {
        private CloudMedContext db = new CloudMedContext();
        private static CloudBlobContainer imagesBlobContainer;
        private CloudQueue doctorQueue; 


        public DoctorsController()
        {
            InitStorage();
        }



        //Init Storage Method
        private void InitStorage()
        {
            var storageAccount = CloudStorageAccount.Parse
            (RoleEnvironment.GetConfigurationSettingValue
            ("StorageConnectionString"));

            var blobClient2 = storageAccount.CreateCloudBlobClient();
            blobClient2.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3); //Set permissions
            imagesBlobContainer = blobClient2.GetContainerReference("doctorimages");
            imagesBlobContainer.CreateIfNotExists();
            imagesBlobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            queueClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

            doctorQueue = queueClient.GetQueueReference("docimages");
            doctorQueue.CreateIfNotExists();
            
        }

        //Upload and Save Blob Method
        private async Task<CloudBlockBlob> UploadAndSaveBlobAsync(HttpPostedFileBase imageFile)//Can be called and reliable under heavy loads
        {
            Trace.TraceInformation("Uploading image file {0}", imageFile.FileName);
            string blobName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            CloudBlockBlob imageBlob = imagesBlobContainer.GetBlockBlobReference(blobName);
            using (var fileStream = imageFile.InputStream)
            {
                await imageBlob.UploadFromStreamAsync(fileStream);
            }
            Trace.TraceInformation("Uploaded image file to {0}", imageBlob.Uri.ToString());
            return imageBlob;
        }

        //Delete Blob Method
        private async Task DeleteBlobAsync(string imageURL)
        {
            if (!string.IsNullOrWhiteSpace(imageURL))
            {
                Uri blobUri = new Uri(imageURL);
                string blobName = blobUri.Segments
                [blobUri.Segments.Length - 1]; Trace.TraceInformation("Deleting image blob {0}", blobName);
                CloudBlockBlob blobToDelete = imagesBlobContainer.
                GetBlockBlobReference(blobName);
                await blobToDelete.DeleteIfExistsAsync();

            }

        }




        /*
         *         public ViewResult Index(string so,string searchString)//so sortOrder 
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(so) ? "Name" : "";
            ViewBag.DateSortParam = so == "DOB" ? "DOB" : "DOB";
            var patients = from p in db.Patients select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                patients = patients.Where(p => p.Name.Contains(searchString) || p.ICNo.Contains(searchString));
            }
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
        */



        // GET: Doctors
        public ViewResult Index(string so,string searchString)
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(so) ? "Name" : "";
            ViewBag.SpecialSort = so == "Specialty" ? "Specialty" : "Specialty";
            var doctors = from p in db.Doctors select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(p => p.Name.Contains(searchString) || p.ICNo.Contains(searchString));
            }
            switch (so)
            {
                case "Name":
                    doctors = doctors.OrderByDescending(p => p.Name);
                    break;
                case "Specialty":
                    doctors = doctors.OrderBy(p => p.Specialty);
                    break;
                default:
                    doctors = doctors.OrderBy(s => s.ICNo);
                    break;
            }
            return View(doctors.ToList());
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DoctorID,Specialty,PersonID,ICNo,Name,Citizenship,EmailAddr,DoctorImageURL")] Doctor doctor, HttpPostedFileBase imageFile)
        {
            CloudBlockBlob imageBlob = null;

            if (ModelState.IsValid)
            {
                bool imageIn = false;
                string queueMsg = "";
                if (imageFile != null && imageFile.ContentLength != 0)
                {
                    imageBlob = await UploadAndSaveBlobAsync(imageFile);
                    doctor.DoctorImageURL = imageBlob.Uri.ToString();
                    Trace.TraceInformation("The Blob URL is:" + imageBlob.Uri.ToString());
                    imageIn = true;
                }
                db.Doctors.Add(doctor);
                await db.SaveChangesAsync();
                queueMsg = doctor.DoctorID.ToString();
                if (imageIn)
                {
                    var queueMessage = new CloudQueueMessage(queueMsg);
                    await doctorQueue.AddMessageAsync(queueMessage);
                }
                return RedirectToAction("Index");
            }

            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DoctorID,Specialty,PersonID,ICNo,Name,Citizenship,EmailAddr,DoctorImageURL")] Doctor doctor, HttpPostedFileBase imageFile)
        {
            CloudBlockBlob imageBlob = null;
            if (ModelState.IsValid)
            {
                bool imageChange = false;
                string queueMsg = "";
                if (imageFile != null && imageFile.ContentLength != 0)
                {
                    await DeleteBlobAsync(doctor.DoctorImageURL);
                    imageBlob = await UploadAndSaveBlobAsync(imageFile);
                    doctor.DoctorImageURL = imageBlob.Uri.ToString();
                }
                db.Entry(doctor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                queueMsg = doctor.DoctorID.ToString();

                if (imageChange)
                {
                    var queueMessage = new CloudQueueMessage(queueMsg);
                    await doctorQueue.AddMessageAsync(queueMessage);
                }
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            await DeleteBlobAsync(doctor.DoctorImageURL);
            db.Doctors.Remove(doctor);
            await db.SaveChangesAsync();
            Trace.TraceInformation("Delete frn {0}", doctor.ICNo);
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
