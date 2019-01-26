namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stilladjustingonMedicalEquipmentvm : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MedicalEquipmentViewModels", "Warranty");
            DropColumn("dbo.MedicalEquipmentViewModels", "PurchaseDate");
            DropColumn("dbo.MedicalEquipmentViewModels", "LastMaintenance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MedicalEquipmentViewModels", "LastMaintenance", c => c.DateTime(nullable: false));
            AddColumn("dbo.MedicalEquipmentViewModels", "PurchaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MedicalEquipmentViewModels", "Warranty", c => c.DateTime(nullable: false));
        }
    }
}
