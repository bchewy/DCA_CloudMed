namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyDoctor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "DoctorImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "DoctorImageURL");
        }
    }
}
