namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userAddRoleId_AddRoleAndPremission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Premissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        pid = c.Int(),
                        PValue = c.String(nullable: false),
                        Url = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Premissions", t => t.pid)
                .Index(t => t.pid);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 50),
                        Authority = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Premissions", "pid", "dbo.Premissions");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Premissions", new[] { "pid" });
            DropColumn("dbo.Users", "RoleId");
            DropTable("dbo.Roles");
            DropTable("dbo.Premissions");
        }
    }
}
