namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groups2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "GroupId", c => c.Int());
            CreateIndex("dbo.Posts", "GroupId");
            AddForeignKey("dbo.Posts", "GroupId", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "GroupId", "dbo.Groups");
            DropIndex("dbo.Posts", new[] { "GroupId" });
            AlterColumn("dbo.Posts", "GroupId", c => c.Int(nullable: false));
        }
    }
}
