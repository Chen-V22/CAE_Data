namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodeName = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 20),
                        LastEditor = c.String(maxLength: 20),
                        LastEditDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sites");
        }
    }
}
