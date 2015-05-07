namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "FromUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Posts", "ToUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Posts", "FromUserId");
            CreateIndex("dbo.Posts", "ToUserId");
            AddForeignKey("dbo.Posts", "FromUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "ToUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ToUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "FromUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Posts", new[] { "ToUserId" });
            DropIndex("dbo.Posts", new[] { "FromUserId" });
            AlterColumn("dbo.Posts", "ToUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Posts", "FromUserId", c => c.Int(nullable: false));
        }
    }
}
