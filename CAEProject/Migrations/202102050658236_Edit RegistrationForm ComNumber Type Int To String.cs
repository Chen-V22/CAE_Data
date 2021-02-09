namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditRegistrationFormComNumberTypeIntToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RegistrationForms", "CompanyNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RegistrationForms", "CompanyNumber", c => c.Int(nullable: false));
        }
    }
}
