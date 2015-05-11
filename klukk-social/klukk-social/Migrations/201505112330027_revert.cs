namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class revert : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Groups", "OpenGroup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "OpenGroup", c => c.Boolean(nullable: false));
        }
    }
}
