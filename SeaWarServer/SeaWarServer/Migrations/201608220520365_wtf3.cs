namespace SeaWarServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wtf3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ships", "TargetPosition");
            DropColumn("dbo.Ships", "Action");
            DropColumn("dbo.Ships", "Owner");
            DropColumn("dbo.Ships", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ships", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Ships", "Owner", c => c.Int());
            AddColumn("dbo.Ships", "Action", c => c.Int());
            AddColumn("dbo.Ships", "TargetPosition", c => c.Int());
        }
    }
}
