namespace Research_science.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zxcv : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customer", "UserName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Customer", "Password", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Customer", "Email", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Employer", "UserName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Employer", "Password", c => c.String(nullable: false, maxLength: 255, unicode: false));
            AlterColumn("dbo.Employer", "Email", c => c.String(nullable: false, maxLength: 255, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employer", "Email", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Employer", "Password", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Employer", "UserName", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Customer", "Email", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Customer", "Password", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.Customer", "UserName", c => c.String(maxLength: 255, unicode: false));
        }
    }
}
