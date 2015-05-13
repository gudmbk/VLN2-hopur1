namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReportItems", "ReportedById", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReportItems", "ReportedById");
        }
    }
}
