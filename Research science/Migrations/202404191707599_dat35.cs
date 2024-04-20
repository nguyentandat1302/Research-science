namespace Research_science.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dat35 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apply", "Accept", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Apply", "Accept");
        }
    }
}
