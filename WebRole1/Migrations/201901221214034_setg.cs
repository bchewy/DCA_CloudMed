namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientViewModels", "PatientImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientViewModels", "PatientImageURL");
        }
    }
}
