namespace Research_science.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tandatq : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        IDCustomer = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 255, unicode: false),
                        Fullname = c.String(nullable: false, maxLength: 255, unicode: false),
                        Email = c.String(nullable: false, maxLength: 255, unicode: false),
                        PhoneNumber = c.String(maxLength: 100, unicode: false),
                        Avatar = c.String(maxLength: 255, unicode: false),
                        Role = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.IDCustomer);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        IDJob = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255, unicode: false),
                        Description = c.String(unicode: false, storeType: "text"),
                        Budget = c.Decimal(precision: 10, scale: 2),
                        Deadline = c.DateTime(storeType: "date"),
                        Status = c.String(maxLength: 50, unicode: false),
                        IDCustomer = c.Int(),
                        IDEmployer = c.Int(),
                    })
                .PrimaryKey(t => t.IDJob)
                .ForeignKey("dbo.Customer", t => t.IDCustomer)
                .ForeignKey("dbo.Employer", t => t.IDEmployer)
                .Index(t => t.IDCustomer)
                .Index(t => t.IDEmployer);
            
            CreateTable(
                "dbo.Employer",
                c => new
                    {
                        IDEmployer = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 255, unicode: false),
                        CompanyName = c.String(nullable: false, maxLength: 255, unicode: false),
                        Email = c.String(nullable: false, maxLength: 255, unicode: false),
                        Phone = c.String(maxLength: 15, unicode: false),
                        Avatar = c.String(maxLength: 255, unicode: false),
                        Role = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.IDEmployer);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        IDMessage = c.Int(nullable: false, identity: true),
                        Content = c.String(unicode: false, storeType: "text"),
                        SendDate = c.DateTime(),
                        IDCustomer = c.Int(),
                        IDEmployer = c.Int(),
                        IDJob = c.Int(),
                    })
                .PrimaryKey(t => t.IDMessage)
                .ForeignKey("dbo.Customer", t => t.IDCustomer)
                .ForeignKey("dbo.Employer", t => t.IDEmployer)
                .ForeignKey("dbo.Job", t => t.IDJob)
                .Index(t => t.IDCustomer)
                .Index(t => t.IDEmployer)
                .Index(t => t.IDJob);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        IDPayment = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(precision: 10, scale: 2),
                        Description = c.String(unicode: false, storeType: "text"),
                        TransactionDate = c.DateTime(),
                        IDJob = c.Int(),
                        IDCustomer = c.Int(),
                        IDEmployer = c.Int(),
                    })
                .PrimaryKey(t => t.IDPayment)
                .ForeignKey("dbo.Customer", t => t.IDCustomer)
                .ForeignKey("dbo.Employer", t => t.IDEmployer)
                .ForeignKey("dbo.Job", t => t.IDJob)
                .Index(t => t.IDJob)
                .Index(t => t.IDCustomer)
                .Index(t => t.IDEmployer);
            
            CreateTable(
                "dbo.Proposal",
                c => new
                    {
                        IDProposal = c.Int(nullable: false),
                        Status = c.String(maxLength: 50, unicode: false),
                        ProposalText = c.String(unicode: false, storeType: "text"),
                        Submission = c.DateTime(),
                        IDJob = c.Int(),
                        IDCustomer = c.Int(),
                    })
                .PrimaryKey(t => t.IDProposal)
                .ForeignKey("dbo.Customer", t => t.IDCustomer)
                .ForeignKey("dbo.Job", t => t.IDJob)
                .Index(t => t.IDJob)
                .Index(t => t.IDCustomer);
            
            CreateTable(
                "dbo.Skill",
                c => new
                    {
                        IDSkill = c.Int(nullable: false, identity: true),
                        SkillName = c.String(maxLength: 255, unicode: false),
                        Experience = c.Int(),
                        IDCustomer = c.Int(),
                    })
                .PrimaryKey(t => t.IDSkill)
                .ForeignKey("dbo.Customer", t => t.IDCustomer)
                .Index(t => t.IDCustomer);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Skill", "IDCustomer", "dbo.Customer");
            DropForeignKey("dbo.Proposal", "IDJob", "dbo.Job");
            DropForeignKey("dbo.Proposal", "IDCustomer", "dbo.Customer");
            DropForeignKey("dbo.Payment", "IDJob", "dbo.Job");
            DropForeignKey("dbo.Payment", "IDEmployer", "dbo.Employer");
            DropForeignKey("dbo.Payment", "IDCustomer", "dbo.Customer");
            DropForeignKey("dbo.Message", "IDJob", "dbo.Job");
            DropForeignKey("dbo.Message", "IDEmployer", "dbo.Employer");
            DropForeignKey("dbo.Message", "IDCustomer", "dbo.Customer");
            DropForeignKey("dbo.Job", "IDEmployer", "dbo.Employer");
            DropForeignKey("dbo.Job", "IDCustomer", "dbo.Customer");
            DropIndex("dbo.Skill", new[] { "IDCustomer" });
            DropIndex("dbo.Proposal", new[] { "IDCustomer" });
            DropIndex("dbo.Proposal", new[] { "IDJob" });
            DropIndex("dbo.Payment", new[] { "IDEmployer" });
            DropIndex("dbo.Payment", new[] { "IDCustomer" });
            DropIndex("dbo.Payment", new[] { "IDJob" });
            DropIndex("dbo.Message", new[] { "IDJob" });
            DropIndex("dbo.Message", new[] { "IDEmployer" });
            DropIndex("dbo.Message", new[] { "IDCustomer" });
            DropIndex("dbo.Job", new[] { "IDEmployer" });
            DropIndex("dbo.Job", new[] { "IDCustomer" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Skill");
            DropTable("dbo.Proposal");
            DropTable("dbo.Payment");
            DropTable("dbo.Message");
            DropTable("dbo.Employer");
            DropTable("dbo.Job");
            DropTable("dbo.Customer");
        }
    }
}
