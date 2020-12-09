namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditMemeberData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "Account", c => c.String(maxLength: 15));
            AlterColumn("dbo.Members", "Password", c => c.String(maxLength: 100));
            AlterColumn("dbo.Members", "Address", c => c.String(maxLength: 50));
            AlterColumn("dbo.Members", "MobilePhone", c => c.String(maxLength: 10));
            AlterColumn("dbo.Members", "IdCard", c => c.String(maxLength: 10));
            AlterColumn("dbo.Members", "Email", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "Email", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Members", "IdCard", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Members", "MobilePhone", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Members", "Address", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Members", "Password", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Members", "Account", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
