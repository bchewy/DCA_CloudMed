namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stilldoingsomeadjusingonmedicalequipmentvm : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicalRecords", "MedicalEquipmentViewModel_EquipmentID", "dbo.MedicalEquipmentViewModels");
            DropIndex("dbo.MedicalRecords", new[] { "MedicalEquipmentViewModel_EquipmentID" });
            DropColumn("dbo.MedicalRecords", "MedicalEquipmentViewModel_EquipmentID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalRecords", "MedicalEquipmentViewModel_EquipmentID", c => c.Int());
            CreateIndex("dbo.MedicalRecords", "MedicalEquipmentViewModel_EquipmentID");
            AddForeignKey("dbo.MedicalRecords", "MedicalEquipmentViewModel_EquipmentID", "dbo.MedicalEquipmentViewModels", "EquipmentID");
        }
    }
}
