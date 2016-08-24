namespace SeaWarServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class typeinship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ships", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ships", "Type");
        }
    }
}
