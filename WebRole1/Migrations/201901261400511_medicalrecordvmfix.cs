namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medicalrecordvmfix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicalEquipmentViewModels",
                c => new
                    {
                        EquipmentID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Brand = c.String(nullable: false),
                        SerialNumber = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        SoftwareVersion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EquipmentID);
            
            AddColumn("dbo.MedicalRecords", "MedicalEquipmentViewModel_EquipmentID", c => c.Int());
            AlterColumn("dbo.Courses", "Title", c => c.String());
            AlterColumn("dbo.MedicalEquipments", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.MedicalEquipments", "Brand", c => c.String(nullable: false));
            AlterColumn("dbo.MedicalEquipments", "SerialNumber", c => c.String(nullable: false));
            AlterColumn("dbo.MedicalEquipments", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.MedicalEquipments", "SoftwareVersion", c => c.String(nullable: false));
            AlterColumn("dbo.MedicalEquipments", "Warranty", c => c.DateTime(nullable: false));
            CreateIndex("dbo.MedicalRecords", "MedicalEquipmentViewModel_EquipmentID");
            AddForeignKey("dbo.MedicalRecords", "MedicalEquipmentViewModel_EquipmentID", "dbo.MedicalEquipmentViewModels", "EquipmentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalRecords", "MedicalEquipmentViewModel_EquipmentID", "dbo.MedicalEquipmentViewModels");
            DropIndex("dbo.MedicalRecords", new[] { "MedicalEquipmentViewModel_EquipmentID" });
            AlterColumn("dbo.MedicalEquipments", "Warranty", c => c.Int(nullable: false));
            AlterColumn("dbo.MedicalEquipments", "SoftwareVersion", c => c.String());
            AlterColumn("dbo.MedicalEquipments", "Status", c => c.String());
            AlterColumn("dbo.MedicalEquipments", "SerialNumber", c => c.String());
            AlterColumn("dbo.MedicalEquipments", "Brand", c => c.String());
            AlterColumn("dbo.MedicalEquipments", "Name", c => c.String());
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false));
            DropColumn("dbo.MedicalRecords", "MedicalEquipmentViewModel_EquipmentID");
            DropTable("dbo.MedicalEquipmentViewModels");
        }
    }
}
