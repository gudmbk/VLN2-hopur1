namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "OpenGroup", c => c.Boolean(nullable: false));
            AddColumn("dbo.Groups", "ProfilePic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "ProfilePic");
            DropColumn("dbo.Groups", "OpenGroup");
        }
    }
}
