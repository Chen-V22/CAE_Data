namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MemberAddPasswordSalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "PasswordSalt", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Members", "PasswordSalt");
        }
    }
}
