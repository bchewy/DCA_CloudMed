namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixDOctor : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doctors", "Doctor_DoctorID", "dbo.Doctors");
            DropIndex("dbo.Doctors", new[] { "Doctor_DoctorID" });
            DropColumn("dbo.Doctors", "Doctor_DoctorID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "Doctor_DoctorID", c => c.Int());
            CreateIndex("dbo.Doctors", "Doctor_DoctorID");
            AddForeignKey("dbo.Doctors", "Doctor_DoctorID", "dbo.Doctors", "DoctorID");
        }
    }
}
