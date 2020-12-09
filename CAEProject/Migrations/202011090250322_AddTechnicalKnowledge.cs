namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTechnicalKnowledge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TechnicalKnowledges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IndustryCategory = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        PublishDateTime = c.DateTime(nullable: false),
                        Source = c.String(nullable: false, maxLength: 20),
                        Clicks = c.Int(nullable: false),
                        ContactPerson = c.String(maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(maxLength: 50),
                        AdStatus = c.Int(nullable: false),
                        Url = c.String(maxLength: 100),
                        IsTop = c.Int(nullable: false),
                        Count = c.String(nullable: false),
                        SDate = c.DateTime(nullable: false),
                        EDate = c.DateTime(nullable: false),
                        Photo = c.String(),
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
            DropTable("dbo.TechnicalKnowledges");
        }
    }
}
