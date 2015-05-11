namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class openGrp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Groups", "OpenGroup", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Groups", "OpenGroup");
        }
    }
}
