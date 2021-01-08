namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResultsPublishAddSource : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResultsPublisheds", "Source", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResultsPublisheds", "Source");
        }
    }
}
