namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Body", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Body");
        }
    }
}
