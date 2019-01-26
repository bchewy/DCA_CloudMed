namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thumbnailDoc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "DoctorThumbnailImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "DoctorThumbnailImageURL");
        }
    }
}
