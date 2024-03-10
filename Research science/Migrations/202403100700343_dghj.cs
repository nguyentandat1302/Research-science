namespace Research_science.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dghj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employer", "CompanyName", c => c.String(nullable: false, maxLength: 255, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employer", "CompanyName", c => c.String(maxLength: 255, unicode: false));
        }
    }
}
