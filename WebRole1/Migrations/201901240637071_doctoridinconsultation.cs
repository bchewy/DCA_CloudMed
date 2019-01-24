namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctoridinconsultation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doctors", "ConsultationID", "dbo.Consultations");
            AddColumn("dbo.Consultations", "DoctorID", c => c.Int(nullable: false));
            AddColumn("dbo.Doctors", "Consultation_ConsultationID", c => c.Int());
            CreateIndex("dbo.Consultations", "DoctorID");
            CreateIndex("dbo.Doctors", "Consultation_ConsultationID");
            AddForeignKey("dbo.Consultations", "DoctorID", "dbo.Doctors", "DoctorID", cascadeDelete: true);
            AddForeignKey("dbo.Doctors", "Consultation_ConsultationID", "dbo.Consultations", "ConsultationID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctors", "Consultation_ConsultationID", "dbo.Consultations");
            DropForeignKey("dbo.Consultations", "DoctorID", "dbo.Doctors");
            DropIndex("dbo.Doctors", new[] { "Consultation_ConsultationID" });
            DropIndex("dbo.Consultations", new[] { "DoctorID" });
            DropColumn("dbo.Doctors", "Consultation_ConsultationID");
            DropColumn("dbo.Consultations", "DoctorID");
            AddForeignKey("dbo.Doctors", "ConsultationID", "dbo.Consultations", "ConsultationID", cascadeDelete: true);
        }
    }
}
