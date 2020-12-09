namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRpShowTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResultsPublisheds", "ShowDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResultsPublisheds", "ShowDate");
        }
    }
}
