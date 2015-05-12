namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tester : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Likes", "PostId");
            AddForeignKey("dbo.Likes", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "PostId", "dbo.Posts");
            DropIndex("dbo.Likes", new[] { "PostId" });
        }
    }
}
