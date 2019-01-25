namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedDiagnosis : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Diagnosis", "ConsultationID", "dbo.Consultations");
            DropForeignKey("dbo.MedicalEquipments", "DiagnosisID", "dbo.Diagnosis");
            DropForeignKey("dbo.MedicalRecords", "DiagnosisID", "dbo.Diagnosis");
            DropIndex("dbo.Diagnosis", new[] { "ConsultationID" });
            DropIndex("dbo.MedicalEquipments", new[] { "DiagnosisID" });
            DropIndex("dbo.MedicalRecords", new[] { "DiagnosisID" });
            CreateTable(
                "dbo.MedicalEquipmentMedicalRecords",
                c => new
                    {
                        MedicalEquipment_EquipmentID = c.Int(nullable: false),
                        MedicalRecord_RecordID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MedicalEquipment_EquipmentID, t.MedicalRecord_RecordID })
                .ForeignKey("dbo.MedicalEquipments", t => t.MedicalEquipment_EquipmentID, cascadeDelete: true)
                .ForeignKey("dbo.MedicalRecords", t => t.MedicalRecord_RecordID, cascadeDelete: true)
                .Index(t => t.MedicalEquipment_EquipmentID)
                .Index(t => t.MedicalRecord_RecordID);
            
            AddColumn("dbo.MedicalRecords", "Illness", c => c.String());
            AddColumn("dbo.MedicalRecords", "ConsultationID", c => c.Int(nullable: false));
            CreateIndex("dbo.MedicalRecords", "ConsultationID");
            AddForeignKey("dbo.MedicalRecords", "ConsultationID", "dbo.Consultations", "ConsultationID", cascadeDelete: true);
            DropColumn("dbo.MedicalEquipments", "DiagnosisID");
            DropColumn("dbo.MedicalRecords", "DiagnosisID");
            DropTable("dbo.Diagnosis");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        DiagnosisID = c.Int(nullable: false, identity: true),
                        Illness = c.String(),
                        Description = c.String(),
                        ConsultationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DiagnosisID);
            
            AddColumn("dbo.MedicalRecords", "DiagnosisID", c => c.Int(nullable: false));
            AddColumn("dbo.MedicalEquipments", "DiagnosisID", c => c.Int(nullable: false));
            DropForeignKey("dbo.MedicalEquipmentMedicalRecords", "MedicalRecord_RecordID", "dbo.MedicalRecords");
            DropForeignKey("dbo.MedicalEquipmentMedicalRecords", "MedicalEquipment_EquipmentID", "dbo.MedicalEquipments");
            DropForeignKey("dbo.MedicalRecords", "ConsultationID", "dbo.Consultations");
            DropIndex("dbo.MedicalEquipmentMedicalRecords", new[] { "MedicalRecord_RecordID" });
            DropIndex("dbo.MedicalEquipmentMedicalRecords", new[] { "MedicalEquipment_EquipmentID" });
            DropIndex("dbo.MedicalRecords", new[] { "ConsultationID" });
            DropColumn("dbo.MedicalRecords", "ConsultationID");
            DropColumn("dbo.MedicalRecords", "Illness");
            DropTable("dbo.MedicalEquipmentMedicalRecords");
            CreateIndex("dbo.MedicalRecords", "DiagnosisID");
            CreateIndex("dbo.MedicalEquipments", "DiagnosisID");
            CreateIndex("dbo.Diagnosis", "ConsultationID");
            AddForeignKey("dbo.MedicalRecords", "DiagnosisID", "dbo.Diagnosis", "DiagnosisID", cascadeDelete: true);
            AddForeignKey("dbo.MedicalEquipments", "DiagnosisID", "dbo.Diagnosis", "DiagnosisID", cascadeDelete: true);
            AddForeignKey("dbo.Diagnosis", "ConsultationID", "dbo.Consultations", "ConsultationID", cascadeDelete: true);
        }
    }
}
