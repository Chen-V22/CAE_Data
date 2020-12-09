namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShowOff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShowOffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        SourceDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        ContactPerson = c.String(nullable: false, maxLength: 20),
                        ContactPhone = c.String(nullable: false, maxLength: 10),
                        ContactEmail = c.String(nullable: false, maxLength: 50),
                        ShowDateTime = c.DateTime(nullable: false),
                        Count = c.String(nullable: false),
                        Photo = c.String(),
                        Clicks = c.Int(nullable: false),
                        AddUser = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                        LastEditDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShowOffs");
        }
    }
}
