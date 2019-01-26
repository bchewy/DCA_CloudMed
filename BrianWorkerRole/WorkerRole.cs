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
        private CloudQueue patientsQueue;
        private CloudBlobContainer imagesBlobContainer;
        //CloudMedContext
        private CloudMedContext db = new CloudMedContext();

        //Others
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        public override void Run()
        {
            Trace.TraceInformation("BrianWorkerRole is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            //read db string and open db
            var dbConnString = CloudConfigurationManager.GetSetting("CloudMedDbConnectionString");
            db = new CloudMedContext(dbConnString);

            //open storage from cscfg
            var storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageConnectionString"));

            //Patients queue
            Trace.TraceInformation("Create patient queue container");
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            patientsQueue = queueClient.GetQueueReference("patients"); //Not used at the moment, since only img->tnail is used
            patientsQueue.CreateIfNotExists();


            //Patient Images
            Trace.TraceInformation("Create images blob container");
            var blobClient = storageAccount.CreateCloudBlobClient();
            imagesBlobContainer = blobClient.GetContainerReference("images");

            if (imagesBlobContainer.CreateIfNotExists())
            {
                //	Enable public access on the images blob container. 
                imagesBlobContainer.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            }


            Trace.TraceInformation("Create images queue container");
            patientsQueue = queueClient.GetQueueReference("images");
            patientsQueue.CreateIfNotExists();

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
