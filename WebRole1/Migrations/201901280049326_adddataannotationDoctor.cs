namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddataannotationDoctor : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "Specialty", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Doctors", "Specialty", c => c.String());
        }
    }
}
