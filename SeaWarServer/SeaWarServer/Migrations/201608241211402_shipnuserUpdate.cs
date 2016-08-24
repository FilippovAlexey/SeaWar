namespace SeaWarServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipnuserUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ships", "HoldSize", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ships", "HoldSize");
        }
    }
}
