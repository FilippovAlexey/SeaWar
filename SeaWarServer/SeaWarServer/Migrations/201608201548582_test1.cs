namespace SeaWarServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ships", "Owner", c => c.Int());
            AlterColumn("dbo.Ships", "Health", c => c.Double(nullable: false));
            AlterColumn("dbo.Ships", "Speed", c => c.Double(nullable: false));
            AlterColumn("dbo.Ships", "Damage", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ships", "Damage", c => c.Int(nullable: false));
            AlterColumn("dbo.Ships", "Speed", c => c.Int(nullable: false));
            AlterColumn("dbo.Ships", "Health", c => c.Int(nullable: false));
            DropColumn("dbo.Ships", "Owner");
        }
    }
}
