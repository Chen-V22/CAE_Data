namespace CAEProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemberAllModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MbCaeSoftwareRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MbId = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                        SoftwareType = c.String(maxLength: 10),
                        Status = c.String(maxLength: 5),
                        Remarks = c.String(maxLength: 150),
                        ReservationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MbId, cascadeDelete: true)
                .Index(t => t.MbId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MbLevelId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Account = c.String(nullable: false, maxLength: 15),
                        Password = c.String(nullable: false, maxLength: 100),
                        ApproveDateTime = c.DateTime(),
                        DisenableDateTime = c.DateTime(),
                        NoticeDateTime = c.DateTime(),
                        CompanyName = c.String(maxLength: 100),
                        CompanyNumber = c.String(maxLength: 8),
                        Principal = c.String(maxLength: 50),
                        PrincipalJobTitle = c.String(maxLength: 20),
                        CompanyPhone = c.String(maxLength: 20),
                        CompanyUrl = c.String(maxLength: 20),
                        ContactPersonJobTitle = c.String(maxLength: 20),
                        EmployeeCount = c.Int(nullable: false),
                        ContactPersonEmail = c.String(maxLength: 100),
                        CompanyType = c.Int(nullable: false),
                        Industry = c.Int(nullable: false),
                        Training = c.Int(nullable: false),
                        CompanyIntroduction = c.String(),
                        Business = c.String(),
                        CompanyPhoto = c.String(),
                        ContactPerson = c.String(maxLength: 50),
                        ContactPersonPhone = c.String(maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 50),
                        Extension = c.Int(nullable: false),
                        Fax = c.String(maxLength: 15),
                        CurrentIdentity = c.String(maxLength: 50),
                        CurrentUnit = c.String(maxLength: 50),
                        JobTitle = c.String(maxLength: 50),
                        MobilePhone = c.String(nullable: false, maxLength: 10),
                        IdCard = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(maxLength: 50),
                        BusinessItem = c.String(),
                        Demand = c.Int(nullable: false),
                        Subscription = c.Int(nullable: false),
                        EditUser = c.String(),
                        LastEditDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MbLevels", t => t.MbLevelId, cascadeDelete: true)
                .Index(t => t.MbLevelId);
            
            CreateTable(
                "dbo.MbLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Price = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        Welfare = c.Int(nullable: false),
                        AddUser = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        EditUser = c.String(),
                        LastEditDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MbPaids",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MbId = c.Int(nullable: false),
                        Paid = c.Int(nullable: false),
                        AddPoints = c.Int(nullable: false),
                        PaidDateTime = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MbId, cascadeDelete: true)
                .Index(t => t.MbId);
            
            CreateTable(
                "dbo.MbPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MbId = c.Int(nullable: false),
                        UseOfHour = c.Int(nullable: false),
                        UsePoints = c.Int(nullable: false),
                        UseDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MbId, cascadeDelete: true)
                .Index(t => t.MbId);
            
            CreateTable(
                "dbo.MbRemarks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MbRemarksId = c.Int(nullable: false),
                        Remark = c.String(),
                        AddUser = c.String(),
                        DateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MbRemarksId, cascadeDelete: true)
                .Index(t => t.MbRemarksId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MbRemarks", "MbRemarksId", "dbo.Members");
            DropForeignKey("dbo.MbPoints", "MbId", "dbo.Members");
            DropForeignKey("dbo.MbPaids", "MbId", "dbo.Members");
            DropForeignKey("dbo.Members", "MbLevelId", "dbo.MbLevels");
            DropForeignKey("dbo.MbCaeSoftwareRecords", "MbId", "dbo.Members");
            DropIndex("dbo.MbRemarks", new[] { "MbRemarksId" });
            DropIndex("dbo.MbPoints", new[] { "MbId" });
            DropIndex("dbo.MbPaids", new[] { "MbId" });
            DropIndex("dbo.Members", new[] { "MbLevelId" });
            DropIndex("dbo.MbCaeSoftwareRecords", new[] { "MbId" });
            DropTable("dbo.MbRemarks");
            DropTable("dbo.MbPoints");
            DropTable("dbo.MbPaids");
            DropTable("dbo.MbLevels");
            DropTable("dbo.Members");
            DropTable("dbo.MbCaeSoftwareRecords");
        }
    }
}
