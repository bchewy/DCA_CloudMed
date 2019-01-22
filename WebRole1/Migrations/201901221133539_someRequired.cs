namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class someRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Consultations", "Status", c => c.String(nullable: false));
            AlterColumn("dbo.Consultations", "ConsultationType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Consultations", "ConsultationType", c => c.String());
            AlterColumn("dbo.Consultations", "Status", c => c.String());
        }
    }
}
