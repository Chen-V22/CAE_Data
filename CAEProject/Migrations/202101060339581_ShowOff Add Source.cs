namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShowOffAddSource : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShowOffs", "Source", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShowOffs", "Source");
        }
    }
}
