namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFaq : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Faqs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FaqStatus = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        SourceDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        ReleaseDateTime = c.DateTime(nullable: false),
                        Clicks = c.Int(nullable: false),
                        Url = c.String(maxLength: 100),
                        Count = c.String(nullable: false),
                        AddUser = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                        LastEditDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Faqs");
        }
    }
}
