namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditUserPSsalt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "PasswordSalt", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PasswordSalt", c => c.String(maxLength: 50));
        }
    }
}
