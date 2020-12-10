namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Tc_Rf_Rm : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegistrationForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrainingId = c.Int(nullable: false),
                        MemberName = c.String(),
                        Company = c.String(maxLength: 100),
                        CompanyNumber = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        Department = c.String(maxLength: 20),
                        JobTitle = c.String(maxLength: 10),
                        Cellphone = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 50),
                        Remarks = c.String(maxLength: 100),
                        GenderType = c.Int(nullable: false),
                        UniformInvoice = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrainingCourses", t => t.TrainingId, cascadeDelete: true)
                .Index(t => t.TrainingId);
            
            CreateTable(
                "dbo.TrainingCourses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Status = c.Int(nullable: false),
                        SeminarStatus = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ContactPerson = c.String(nullable: false, maxLength: 20),
                        ContactPhone = c.String(nullable: false, maxLength: 10),
                        ContactEmail = c.String(nullable: false, maxLength: 50),
                        SDate = c.DateTime(nullable: false),
                        EDate = c.DateTime(nullable: false),
                        SignUpSDate = c.DateTime(nullable: false),
                        SignUpEDate = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 50),
                        Quota = c.Int(nullable: false),
                        Alternate = c.Int(nullable: false),
                        Condition = c.Int(nullable: false),
                        Handle = c.Int(nullable: false),
                        Assisting = c.String(maxLength: 20),
                        ProjectName = c.String(maxLength: 20),
                        Count = c.String(nullable: false),
                        File = c.String(),
                        Description = c.String(maxLength: 300),
                        Success = c.String(maxLength: 300),
                        AdImage = c.String(),
                        Clicks = c.Int(nullable: false),
                        AddUser = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                        LastEditDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RegistrationMinors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrainingCourseId = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                        Name = c.String(maxLength: 20),
                        Length = c.Int(nullable: false),
                        IsRequired = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrainingCourses", t => t.TrainingCourseId, cascadeDelete: true)
                .Index(t => t.TrainingCourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegistrationMinors", "TrainingCourseId", "dbo.TrainingCourses");
            DropForeignKey("dbo.TrainingCourses", "UserId", "dbo.Users");
            DropForeignKey("dbo.RegistrationForms", "TrainingId", "dbo.TrainingCourses");
            DropIndex("dbo.RegistrationMinors", new[] { "TrainingCourseId" });
            DropIndex("dbo.TrainingCourses", new[] { "UserId" });
            DropIndex("dbo.RegistrationForms", new[] { "TrainingId" });
            DropTable("dbo.RegistrationMinors");
            DropTable("dbo.TrainingCourses");
            DropTable("dbo.RegistrationForms");
        }
    }
}
