namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consultations",
                c => new
                    {
                        ConsultationID = c.Int(nullable: false, identity: true),
                        QueueNo = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        Status = c.String(),
                        ConsultationType = c.String(),
                        PatientID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConsultationID)
                .ForeignKey("dbo.Patients", t => t.PatientID, cascadeDelete: true)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        DiagnosisID = c.Int(nullable: false, identity: true),
                        Illness = c.String(),
                        Description = c.String(),
                        ConsultationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiagnosisID)
                .ForeignKey("dbo.Consultations", t => t.ConsultationID, cascadeDelete: true)
                .Index(t => t.ConsultationID);
            
            CreateTable(
                "dbo.MedicalEquipments",
                c => new
                    {
                        EquipmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Brand = c.String(),
                        SerialNumber = c.String(),
                        Status = c.String(),
                        SoftwareVersion = c.String(),
                        Warranty = c.Int(nullable: false),
                        PurchaseDate = c.DateTime(nullable: false),
                        LastMaintenance = c.DateTime(nullable: false),
                        DiagnosisID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EquipmentID)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisID, cascadeDelete: true)
                .Index(t => t.DiagnosisID);
            
            CreateTable(
                "dbo.MedicalRecords",
                c => new
                    {
                        RecordID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DocURL = c.String(),
                        DiagnosisID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecordID)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisID, cascadeDelete: true)
                .Index(t => t.DiagnosisID);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorID = c.Int(nullable: false, identity: true),
                        ConsultationID = c.Int(nullable: false),
                        Specialty = c.String(),
                        FacultyID = c.Int(nullable: false),
                        PersonID = c.Int(nullable: false),
                        ICNo = c.String(),
                        Name = c.String(),
                        Citizenship = c.String(),
                        EmailAddr = c.String(),
                    })
                .PrimaryKey(t => t.DoctorID)
                .ForeignKey("dbo.Consultations", t => t.ConsultationID, cascadeDelete: true)
                .Index(t => t.ConsultationID);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Category = c.String(),
                        CourseFee = c.Double(nullable: false),
                        DoctorID = c.Int(nullable: false),
                        Duration = c.Int(),
                        Date = c.DateTime(),
                        Time = c.DateTime(),
                        Capacity = c.Int(),
                        Venue = c.String(),
                        URL = c.String(),
                        DateReleased = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CourseID)
                .ForeignKey("dbo.Doctors", t => t.DoctorID, cascadeDelete: true)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        DoB = c.DateTime(nullable: false),
                        PersonID = c.Int(nullable: false),
                        ICNo = c.String(),
                        Name = c.String(),
                        Citizenship = c.String(),
                        EmailAddr = c.String(),
                    })
                .PrimaryKey(t => t.PatientID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Consultations", "PatientID", "dbo.Patients");
            DropForeignKey("dbo.Courses", "DoctorID", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "ConsultationID", "dbo.Consultations");
            DropForeignKey("dbo.MedicalRecords", "DiagnosisID", "dbo.Diagnosis");
            DropForeignKey("dbo.MedicalEquipments", "DiagnosisID", "dbo.Diagnosis");
            DropForeignKey("dbo.Diagnosis", "ConsultationID", "dbo.Consultations");
            DropIndex("dbo.Courses", new[] { "DoctorID" });
            DropIndex("dbo.Doctors", new[] { "ConsultationID" });
            DropIndex("dbo.MedicalRecords", new[] { "DiagnosisID" });
            DropIndex("dbo.MedicalEquipments", new[] { "DiagnosisID" });
            DropIndex("dbo.Diagnosis", new[] { "ConsultationID" });
            DropIndex("dbo.Consultations", new[] { "PatientID" });
            DropTable("dbo.Patients");
            DropTable("dbo.Courses");
            DropTable("dbo.Doctors");
            DropTable("dbo.MedicalRecords");
            DropTable("dbo.MedicalEquipments");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.Consultations");
        }
    }
}
