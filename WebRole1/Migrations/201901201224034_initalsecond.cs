namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initalsecond : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientViewModels",
                c => new
                    {
                        PatientID = c.Int(nullable: false, identity: true),
                        ICNo = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Citizenship = c.String(nullable: false),
                        EmailAddr = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        DoB = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PatientID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PatientViewModels");
        }
    }
}
