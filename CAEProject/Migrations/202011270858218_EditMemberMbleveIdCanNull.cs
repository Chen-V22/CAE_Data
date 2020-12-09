namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditMemberMbleveIdCanNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Members", "MbLevelId", "dbo.MbLevels");
            DropIndex("dbo.Members", new[] { "MbLevelId" });
            AlterColumn("dbo.Members", "MbLevelId", c => c.Int());
            CreateIndex("dbo.Members", "MbLevelId");
            AddForeignKey("dbo.Members", "MbLevelId", "dbo.MbLevels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "MbLevelId", "dbo.MbLevels");
            DropIndex("dbo.Members", new[] { "MbLevelId" });
            AlterColumn("dbo.Members", "MbLevelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Members", "MbLevelId");
            AddForeignKey("dbo.Members", "MbLevelId", "dbo.MbLevels", "Id", cascadeDelete: true);
        }
    }
}
