namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groups1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromUserId = c.String(maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FromUserId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.FromUserId)
                .Index(t => t.GroupId);
            
            DropColumn("dbo.ReportItems", "ReportedById");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReportItems", "ReportedById", c => c.String());
            DropForeignKey("dbo.GroupRequests", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupRequests", "FromUserId", "dbo.AspNetUsers");
            DropIndex("dbo.GroupRequests", new[] { "GroupId" });
            DropIndex("dbo.GroupRequests", new[] { "FromUserId" });
            DropTable("dbo.GroupRequests");
        }
    }
}
