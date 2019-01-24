namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixEF : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doctors", "ConsultationID", "dbo.Consultations");
            DropForeignKey("dbo.Doctors", "Consultation_ConsultationID", "dbo.Consultations");
            DropIndex("dbo.Doctors", new[] { "ConsultationID" });
            DropIndex("dbo.Doctors", new[] { "Consultation_ConsultationID" });
            AddColumn("dbo.Doctors", "Doctor_DoctorID", c => c.Int());
            CreateIndex("dbo.Doctors", "Doctor_DoctorID");
            AddForeignKey("dbo.Doctors", "Doctor_DoctorID", "dbo.Doctors", "DoctorID");
            DropColumn("dbo.Doctors", "ConsultationID");
            DropColumn("dbo.Doctors", "Consultation_ConsultationID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "Consultation_ConsultationID", c => c.Int());
            AddColumn("dbo.Doctors", "ConsultationID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Doctors", "Doctor_DoctorID", "dbo.Doctors");
            DropIndex("dbo.Doctors", new[] { "Doctor_DoctorID" });
            DropColumn("dbo.Doctors", "Doctor_DoctorID");
            CreateIndex("dbo.Doctors", "Consultation_ConsultationID");
            CreateIndex("dbo.Doctors", "ConsultationID");
            AddForeignKey("dbo.Doctors", "Consultation_ConsultationID", "dbo.Consultations", "ConsultationID");
            AddForeignKey("dbo.Doctors", "ConsultationID", "dbo.Consultations", "ConsultationID", cascadeDelete: true);
        }
    }
}
