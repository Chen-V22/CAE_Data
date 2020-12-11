namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropTcCondition : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TrainingCourses", "Condition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TrainingCourses", "Condition", c => c.Int(nullable: false));
        }
    }
}
