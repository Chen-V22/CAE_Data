namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditSite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sites", "AddUser", c => c.String());
            AddColumn("dbo.Sites", "DateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Sites", "EditUser", c => c.String());
            DropColumn("dbo.Sites", "LastEditor");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sites", "LastEditor", c => c.String(maxLength: 20));
            DropColumn("dbo.Sites", "EditUser");
            DropColumn("dbo.Sites", "DateTime");
            DropColumn("dbo.Sites", "AddUser");
        }
    }
}
