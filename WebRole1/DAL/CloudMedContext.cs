using WebRole1.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebRole1.DAL
{
    public class CloudMedContext : DbContext
    {

        public CloudMedContext() : base("CloudMedContext")
        {
        }

        //Added for worker role
        public CloudMedContext(string dbConnString) : base(dbConnString)
        {
            this.Database.CommandTimeout = 300;
        }

        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalEquipment> MedicalEquipment { get; set; }
        public DbSet<MedicalRecord> MedicalRecord { get; set; }
        public DbSet<Patient> Patients { get; set; }

        public System.Data.Entity.DbSet<WebRole1.Models.PatientViewModel> PatientViewModels { get; set; }

        public System.Data.Entity.DbSet<WebRole1.Models.MedicalEquipmentViewModel> MedicalEquipmentViewModels { get; set; }
        //Table for Person, Seminar or Webinar?

    }
}