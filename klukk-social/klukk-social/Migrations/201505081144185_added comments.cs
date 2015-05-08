namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcomments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ParentId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "ParentId");
        }
    }
}
