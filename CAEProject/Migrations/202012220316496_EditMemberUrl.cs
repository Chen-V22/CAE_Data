namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditMemberUrl : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "CompanyUrl", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "CompanyUrl", c => c.String(maxLength: 20));
        }
    }
}
