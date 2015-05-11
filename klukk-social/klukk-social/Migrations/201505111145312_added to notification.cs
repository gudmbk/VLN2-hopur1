namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtonotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Seen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Seen");
        }
    }
}
