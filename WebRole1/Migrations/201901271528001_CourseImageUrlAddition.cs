namespace WebRole1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseImageUrlAddition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CourseImageURL", c => c.String());
            AddColumn("dbo.Courses", "CourseThumbnailImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "CourseThumbnailImageURL");
            DropColumn("dbo.Courses", "CourseImageURL");
        }
    }
}
