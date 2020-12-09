namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProjectServices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceStatus = c.Int(nullable: false),
                        ServiceType = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 30),
                        Consultant = c.String(maxLength: 30),
                        Content = c.String(nullable: false),
                        Annex = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Founder = c.String(),
                        LastEditDateTime = c.DateTime(nullable: false),
                        LastEditor = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProjectServices");
        }
    }
}
