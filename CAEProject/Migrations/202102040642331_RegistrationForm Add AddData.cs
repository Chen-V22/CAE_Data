namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegistrationFormAddAddData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RegistrationForms", "AddData", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RegistrationForms", "AddData");
        }
    }
}
