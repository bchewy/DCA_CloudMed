namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patientImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "PatientImageURL", c => c.String());
            AddColumn("dbo.Patients", "PatientThumbNailURl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "PatientThumbNailURl");
            DropColumn("dbo.Patients", "PatientImageURL");
        }
    }
}
