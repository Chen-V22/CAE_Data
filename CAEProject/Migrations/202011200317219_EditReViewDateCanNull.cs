namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditReViewDateCanNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ResultsPublisheds", "ReviewDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ResultsPublisheds", "ReviewDate", c => c.DateTime(nullable: false));
        }
    }
}
