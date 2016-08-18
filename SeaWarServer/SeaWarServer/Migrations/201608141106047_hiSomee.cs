namespace SeaWarServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hiSomee : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Ships");
            AlterColumn("dbo.Ships", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Ships", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Ships");
            AlterColumn("dbo.Ships", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Ships", "Id");
        }
    }
}
