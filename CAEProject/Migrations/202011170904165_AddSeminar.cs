namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeminar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Seminars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeminarStatus = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        ShowDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Lecturer = c.String(maxLength: 100),
                        IsTop = c.Int(nullable: false),
                        Clicks = c.Int(nullable: false),
                        Url = c.String(maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 50),
                        Organizer = c.String(nullable: false, maxLength: 50),
                        Assisting = c.String(maxLength: 50),
                        Count = c.String(nullable: false),
                        File = c.String(),
                        SDate = c.DateTime(nullable: false),
                        EDate = c.DateTime(nullable: false),
                        AddUser = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                        LastEditDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Seminars");
        }
    }
}
