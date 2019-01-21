namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeFaculyID : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Doctors", "FacultyID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "FacultyID", c => c.Int(nullable: false));
        }
    }
}
