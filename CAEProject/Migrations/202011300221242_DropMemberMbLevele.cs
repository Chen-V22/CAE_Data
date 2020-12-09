namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropMemberMbLevele : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Members", "MbLevelId", "dbo.MbLevels");
            DropIndex("dbo.Members", new[] { "MbLevelId" });
            AddColumn("dbo.Members", "MemberLevel", c => c.Int(nullable: false));
            DropColumn("dbo.Members", "MbLevelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Members", "MbLevelId", c => c.Int());
            DropColumn("dbo.Members", "MemberLevel");
            CreateIndex("dbo.Members", "MbLevelId");
            AddForeignKey("dbo.Members", "MbLevelId", "dbo.MbLevels", "Id");
        }
    }
}
