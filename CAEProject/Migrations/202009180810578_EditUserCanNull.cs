namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditUserCanNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "PasswordSalt", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PasswordSalt", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
