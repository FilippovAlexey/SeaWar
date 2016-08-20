namespace SeaWarServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ships", "TargetPosition", c => c.Int());
            DropColumn("dbo.Ships", "TargetId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ships", "TargetId", c => c.String());
            DropColumn("dbo.Ships", "TargetPosition");
        }
    }
}
