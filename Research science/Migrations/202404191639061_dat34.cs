namespace Research_science.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dat34 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Job", "Anh", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Job", "Anh");
        }
    }
}
