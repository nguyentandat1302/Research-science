namespace Research_science.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qsss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employer", "Description", c => c.String(maxLength: 255));
            AddColumn("dbo.Employer", "Address", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employer", "Address");
            DropColumn("dbo.Employer", "Description");
        }
    }
}
