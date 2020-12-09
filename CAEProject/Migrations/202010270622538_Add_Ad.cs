namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Ad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdCategory = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                        Photo = c.String(),
                        Content = c.String(),
                        Applican = c.String(nullable: false),
                        AdStatus = c.Int(nullable: false),
                        Url = c.String(),
                        ClickRate = c.Int(),
                        SDate = c.DateTime(nullable: false),
                        EDate = c.DateTime(nullable: false),
                        Annex = c.String(),
                        IsTop = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        LastEditDateTime = c.DateTime(nullable: false),
                        AddUser = c.String(),
                        EditUser = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ads");
        }
    }
}
