namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mabeyadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "ApplicationStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "ApplicationStatus");
        }
    }
}
