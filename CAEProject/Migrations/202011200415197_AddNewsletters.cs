namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewsletters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Newsletters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Num = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
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
            DropTable("dbo.Newsletters");
        }
    }
}
