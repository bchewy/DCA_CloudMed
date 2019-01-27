namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class redoonmedicalequipmentvm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MedicalEquipmentViewModels", "Warranty", c => c.DateTime(nullable: false));
            AddColumn("dbo.MedicalEquipmentViewModels", "PurchaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.MedicalEquipmentViewModels", "LastMaintenance", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MedicalEquipmentViewModels", "LastMaintenance");
            DropColumn("dbo.MedicalEquipmentViewModels", "PurchaseDate");
            DropColumn("dbo.MedicalEquipmentViewModels", "Warranty");
        }
    }
}
