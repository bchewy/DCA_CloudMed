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
    public class PatientsController : Controller
    {
        private CloudMedContext db = new CloudMedContext();
        private static CloudBlobContainer imagesBlobContainer;
        private CloudQueue patientQueue; //Not intended as literal "patientQueue" --> This is a queue for images --> thumbnails 

        public PatientsController()
        {
            InitStorage();
        }
        //Init Storage Method
        private void InitStorage()
        {
            var storageAccount = CloudStorageAccount.Parse
            (RoleEnvironment.GetConfigurationSettingValue
            ("StorageConnectionString"));
        
            var blobClient = storageAccount.CreateCloudBlobClient();
            blobClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3); //Set permissions
            imagesBlobContainer = blobClient.GetContainerReference("patientimages");
            imagesBlobContainer.CreateIfNotExists();
            imagesBlobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            queueClient.DefaultRequestOptions.RetryPolicy = new LinearRetry(TimeSpan.FromSeconds(3), 3);

            patientQueue = queueClient.GetQueueReference("images");
            patientQueue.CreateIfNotExists();
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
        public async Task<ActionResult> Create([Bind(Include = "PatientID,Address,DoB,PersonID,ICNo,Name,Citizenship,EmailAddr,PatientImageURL")] PatientViewModel PatientViewModel,HttpPostedFileBase imageFile)
        {
            bool imageIn = false;
            string queueMsg = "";
            CloudBlockBlob imageBlob = null;
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.ContentLength != 0)
                {
                    imageBlob = await UploadAndSaveBlobAsync(imageFile);
                    PatientViewModel.PatientImageURL = imageBlob.Uri.ToString();
                    Trace.TraceInformation("The Blob URL is:" + imageBlob.Uri.ToString());
                    imageIn = true;
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
                await db.SaveChangesAsync();
                queueMsg = patient.ICNo.ToString(); //Adds Patient's IC to queue
                if (imageIn)
                {
                    var queueMessage = new CloudQueueMessage(queueMsg);
                    await patientQueue.AddMessageAsync(queueMessage);
                }
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
        public async Task<ActionResult> Edit([Bind(Include = "PatientID,Address,DoB,PersonID,ICNo,Name,Citizenship,EmailAddr,PatientImageURl")] PatientViewModel PatientViewModel, HttpPostedFileBase imageFile)
        {
            CloudBlockBlob imageBlob = null;
            if (ModelState.IsValid)
            {
                bool imageChange = false;
                string queueMsg = "";
                if (imageFile != null && imageFile.ContentLength != 0)
                {

                    await DeleteBlobAsync(PatientViewModel.PatientImageURL);
                    imageBlob = await UploadAndSaveBlobAsync(imageFile);
                    PatientViewModel.PatientImageURL = imageBlob.Uri.ToString();
                }

                var patient = db.Patients.Find(PatientViewModel.PatientID);

                patient.ICNo = PatientViewModel.ICNo;
                patient.Name = PatientViewModel.Name;
                patient.Gender = PatientViewModel.Gender;
                patient.Citizenship = PatientViewModel.Citizenship;
                patient.EmailAddr = PatientViewModel.EmailAddr;
                patient.Address = PatientViewModel.Address;
                patient.DoB = PatientViewModel.DoB;
                patient.PatientImageURL = PatientViewModel.PatientImageURL;
                queueMsg = patient.ICNo.ToString();//PatientID to queue

                db.Entry(patient).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (imageChange)
                {
                    var queueMessage = new CloudQueueMessage(queueMsg);
                    await patientQueue.AddMessageAsync(queueMessage);
                }
                return RedirectToAction("Index");
            }
            return View(PatientViewModel);
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
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Patient patient = db.Patients.Find(id);
            await DeleteBlobAsync(patient.PatientImageURL);
            db.Patients.Remove(patient);
            await db.SaveChangesAsync();
            Trace.TraceInformation("Delete {0}", patient.PatientID);
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
