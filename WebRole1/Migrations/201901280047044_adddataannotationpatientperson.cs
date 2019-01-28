namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddataannotationpatientperson : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Doctors", "ICNo", c => c.String(nullable: false));
            AlterColumn("dbo.Doctors", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Doctors", "Citizenship", c => c.String(nullable: false));
            AlterColumn("dbo.Doctors", "EmailAddr", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "ICNo", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "Citizenship", c => c.String(nullable: false));
            AlterColumn("dbo.Patients", "EmailAddr", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Patients", "EmailAddr", c => c.String());
            AlterColumn("dbo.Patients", "Citizenship", c => c.String());
            AlterColumn("dbo.Patients", "Name", c => c.String());
            AlterColumn("dbo.Patients", "ICNo", c => c.String());
            AlterColumn("dbo.Patients", "Address", c => c.String());
            AlterColumn("dbo.Doctors", "EmailAddr", c => c.String());
            AlterColumn("dbo.Doctors", "Citizenship", c => c.String());
            AlterColumn("dbo.Doctors", "Name", c => c.String());
            AlterColumn("dbo.Doctors", "ICNo", c => c.String());
        }
    }
}
