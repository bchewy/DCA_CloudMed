namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testingmedicalequipmentindex : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MedicalEquipmentViewModels");
        }
    }
}
