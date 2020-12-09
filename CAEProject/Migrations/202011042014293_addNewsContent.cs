namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNewsContent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        SourceDate = c.DateTime(nullable: false),
                        Source = c.String(nullable: false, maxLength: 20),
                        Url = c.String(maxLength: 100),
                        Clicks = c.Int(nullable: false),
                        Photo = c.String(),
                        IsTop = c.Int(nullable: false),
                        SDate = c.DateTime(nullable: false),
                        EDate = c.DateTime(nullable: false),
                        Count = c.String(nullable: false),
                        File = c.String(),
                        AddUser = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                        LastEditDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
        }
    }
}
