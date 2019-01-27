namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesomeattributenameonmedicalequpment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalEquipments", "MaintenanceDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MedicalEquipmentViewModels", "MaintenanceDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.MedicalEquipments", "LastMaintenance");
            DropColumn("dbo.MedicalEquipmentViewModels", "LastMaintenance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalEquipmentViewModels", "LastMaintenance", c => c.DateTime(nullable: false));
            AddColumn("dbo.MedicalEquipments", "LastMaintenance", c => c.DateTime(nullable: false));
            DropColumn("dbo.MedicalEquipmentViewModels", "MaintenanceDate");
            DropColumn("dbo.MedicalEquipments", "MaintenanceDate");
        }
    }
}
