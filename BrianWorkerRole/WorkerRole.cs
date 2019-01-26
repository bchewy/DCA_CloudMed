using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using System.IO;
using WebRole1.DAL;
using WebRole1.Models;
using System.Data.Entity;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Net.Mail;



namespace BrianWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        //Initalise patientQueue and BlobContainer
        private CloudQueue patientQueue;
        private CloudBlobContainer imagesBlobContainer;
        //CloudMedContext
        private CloudMedContext db = new CloudMedContext();

        //Others
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        //Workerclass methods

        // ProcessImage
        private void ProcessPatientQueueMsg(CloudQueueMessage msg)
        {

            var PatientIC = int.Parse(msg.AsString);
            Patient pat = db.Patients.Find(PatientIC);

            if (pat == null)
            {
                this.patientQueue.DeleteMessage(msg); throw new Exception(String.Format(
                "VehicleID {0} not found, can't create thumbnail", PatientIC.ToString()));
            }
            Uri blobUri = new Uri(pat.PatientImageURL);
            string blobName =
            blobUri.Segments[blobUri.Segments.Length - 1];
            CloudBlockBlob inputBlob = this.imagesBlobContainer.GetBlockBlobReference(blobName);

            string thumbnailName = Path.GetFileNameWithoutExtension(inputBlob.Name) + "thumb.jpg";
            CloudBlockBlob outputBlob = this.imagesBlobContainer.GetBlockBlobReference(thumbnailName);

            using (Stream input = inputBlob.OpenRead())
            using (Stream output = outputBlob.OpenWrite())
            {
                ConvertImageToThumbnailJPG(input, output);
                outputBlob.Properties.ContentType = "image/jpeg";
            }
            pat.PatientThumbNailURl = outputBlob.Uri.ToString();
            db.Entry(pat).State = EntityState.Modified;
            db.SaveChanges();

            this.patientQueue.DeleteMessage(msg);
        }
        //Process Image to thumbnail
        public void ConvertImageToThumbnailJPG(Stream input, Stream output)
        {
            int thumbnailsize = 240, width, height;
            var originalImage = new Bitmap(input);

            if (originalImage.Width > originalImage.Height)
            {
                width = thumbnailsize;
                height = thumbnailsize *
                originalImage.Height / originalImage.Width;
            }
            else
            {
                height = thumbnailsize;
                width = thumbnailsize *
                originalImage.Width / originalImage.Height;
            }
            Bitmap thumbnailImage = null;
            try
            {
                thumbnailImage = new Bitmap(width, height);
                using (Graphics graphics = Graphics.FromImage(thumbnailImage))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(originalImage, 0, 0, width, height);
                }
                thumbnailImage.Save(output, ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                Trace.TraceError("Error in creating thumbnail image "
                + e.ToString());
            }
            finally
            {
                if (thumbnailImage != null)
                    thumbnailImage.Dispose();
            }

        }


        public override void Run()
        {
            Trace.TraceInformation("BrianWorkerRole is running");
            CloudQueueMessage patientMsg = null;
            while (true)
            {
                try
                {
                    patientMsg = this.patientQueue.GetMessage();
                    if (patientMsg != null)
                    {
                        Debug.WriteLine("PatientIC from Queue:" + patientMsg.AsString);
                        ProcessPatientQueueMsg(patientMsg);
                    }
                    if (patientMsg == null)
                    {
                        System.Threading.Thread.Sleep(10000); //Sleeps for 10 seconds
                    }
                }
                catch(StorageException se)
                {
                    if(patientMsg!=null && patientMsg.DequeueCount > 5)
                    {
                        this.patientQueue.DeleteMessage(patientMsg);
                        Trace.TraceError("Deleting poison queue item " + "from patientQueue: {0}", patientMsg.AsString);
                    }
                    Trace.TraceError("Exception in BrianWorkerRole: '{0}'", se.Message);
                    System.Threading.Thread.Sleep(10000);
                }
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            //read db string and open db
            var dbConnString = CloudConfigurationManager.GetSetting("CloudMedDbConnectionString");
            db = new CloudMedContext(dbConnString);

            //open storage from cscfg
            var storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));

            Trace.TraceInformation("Create patient queue container");
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            //Patient Images blob
            Trace.TraceInformation("Create images blob container");
            var blobClient = storageAccount.CreateCloudBlobClient();
            imagesBlobContainer = blobClient.GetContainerReference("patientimages");

            if (imagesBlobContainer.CreateIfNotExists())
            {
                imagesBlobContainer.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            }
            Trace.TraceInformation("Create images queue container");
            patientQueue = queueClient.GetQueueReference("images");
            patientQueue.CreateIfNotExists();

            Trace.TraceInformation("Storage initialized");
            //End of storage initalisation.


            bool result = base.OnStart();

            Trace.TraceInformation("BrianWorkerRole has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("BrianWorkerRole is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("BrianWorkerRole has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Working");
                await Task.Delay(1000);
            }
        }
    }
}
