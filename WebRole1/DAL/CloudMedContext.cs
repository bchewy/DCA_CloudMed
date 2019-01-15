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

        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalEquipment> MedicalEquipment { get; set; }
        public DbSet<MedicalRecord> MedicalRecord { get; set; }
        public DbSet<Patient> Patients { get; set; }
        //Table for Person, Seminar or Webinar?

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}