namespace SeaWarServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ships", "TargetId", c => c.String());
            AddColumn("dbo.Ships", "Action", c => c.Int());
            AddColumn("dbo.Ships", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ships", "Discriminator");
            DropColumn("dbo.Ships", "Action");
            DropColumn("dbo.Ships", "TargetId");
        }
    }
}
