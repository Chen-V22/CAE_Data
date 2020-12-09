namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPsReply_And_AddNPInPs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PsReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PsId = c.Int(nullable: false),
                        ServiceStatus = c.Int(nullable: false),
                        Reply = c.String(maxLength: 300),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProjectServices", t => t.PsId, cascadeDelete: true)
                .Index(t => t.PsId);
            
            DropColumn("dbo.ProjectServices", "ServiceStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProjectServices", "ServiceStatus", c => c.Int(nullable: false));
            DropForeignKey("dbo.PsReplies", "PsId", "dbo.ProjectServices");
            DropIndex("dbo.PsReplies", new[] { "PsId" });
            DropTable("dbo.PsReplies");
        }
    }
}
