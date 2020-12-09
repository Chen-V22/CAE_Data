namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddARAndAP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityPhotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        Photo = c.String(),
                        PhotoAnnotation = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ActivityRecords", t => t.ActivityId, cascadeDelete: true)
                .Index(t => t.ActivityId);
            
            CreateTable(
                "dbo.ActivityRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        ActivityStatus = c.Int(nullable: false),
                        PublishDateTime = c.DateTime(nullable: false),
                        Source = c.String(nullable: false, maxLength: 20),
                        Clicks = c.Int(nullable: false),
                        Photo = c.String(),
                        IsTop = c.Int(nullable: false),
                        SDate = c.DateTime(nullable: false),
                        EDate = c.DateTime(nullable: false),
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
            DropForeignKey("dbo.ActivityPhotoes", "ActivityId", "dbo.ActivityRecords");
            DropIndex("dbo.ActivityPhotoes", new[] { "ActivityId" });
            DropTable("dbo.ActivityRecords");
            DropTable("dbo.ActivityPhotoes");
        }
    }
}
