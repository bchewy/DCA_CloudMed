namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kw : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MedicalRecords", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MedicalRecords", "Description", c => c.String());
        }
    }
}
