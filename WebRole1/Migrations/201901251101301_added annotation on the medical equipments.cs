namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedannotationonthemedicalequipments : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MedicalEquipments", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.MedicalEquipments", "Brand", c => c.String(nullable: false));
            AlterColumn("dbo.MedicalEquipments", "SerialNumber", c => c.String(nullable: false));
            AlterColumn("dbo.MedicalEquipments", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.MedicalEquipments", "SoftwareVersion", c => c.String(nullable: false));
            AlterColumn("dbo.MedicalEquipments", "Warranty", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MedicalEquipments", "Warranty", c => c.Int(nullable: false));
            AlterColumn("dbo.MedicalEquipments", "SoftwareVersion", c => c.String());
            AlterColumn("dbo.MedicalEquipments", "Status", c => c.String());
            AlterColumn("dbo.MedicalEquipments", "SerialNumber", c => c.String());
            AlterColumn("dbo.MedicalEquipments", "Brand", c => c.String());
            AlterColumn("dbo.MedicalEquipments", "Name", c => c.String());
        }
    }
}
