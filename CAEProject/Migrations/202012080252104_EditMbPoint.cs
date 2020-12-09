namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditMbPoint : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MbPoints", "Points", c => c.Int(nullable: false));
            AlterColumn("dbo.MbPoints", "UseOfHour", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MbPoints", "UseOfHour", c => c.Int(nullable: false));
            DropColumn("dbo.MbPoints", "Points");
        }
    }
}
