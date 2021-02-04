namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TrainingCourseAddApplicationStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrainingCourses", "ApplicationStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrainingCourses", "ApplicationStatus");
        }
    }
}
