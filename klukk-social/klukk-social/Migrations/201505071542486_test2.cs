namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FriendRequests", "FromUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.FriendRequests", "ToUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Friendships", "FromUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Friendships", "ToUserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Groups", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.GroupUsers", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Likes", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Notifications", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Settings", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.FriendRequests", "FromUserId");
            CreateIndex("dbo.FriendRequests", "ToUserId");
            CreateIndex("dbo.Friendships", "FromUserId");
            CreateIndex("dbo.Friendships", "ToUserId");
            CreateIndex("dbo.Groups", "UserId");
            CreateIndex("dbo.GroupUsers", "UserId");
            CreateIndex("dbo.Likes", "UserId");
            CreateIndex("dbo.Notifications", "UserId");
            CreateIndex("dbo.Settings", "UserId");
            AddForeignKey("dbo.FriendRequests", "FromUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FriendRequests", "ToUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Friendships", "FromUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Friendships", "ToUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Groups", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.GroupUsers", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Notifications", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Settings", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Settings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupUsers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Groups", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friendships", "ToUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friendships", "FromUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "ToUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "FromUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Settings", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropIndex("dbo.Likes", new[] { "UserId" });
            DropIndex("dbo.GroupUsers", new[] { "UserId" });
            DropIndex("dbo.Groups", new[] { "UserId" });
            DropIndex("dbo.Friendships", new[] { "ToUserId" });
            DropIndex("dbo.Friendships", new[] { "FromUserId" });
            DropIndex("dbo.FriendRequests", new[] { "ToUserId" });
            DropIndex("dbo.FriendRequests", new[] { "FromUserId" });
            AlterColumn("dbo.Settings", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Notifications", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Likes", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.GroupUsers", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Groups", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Friendships", "ToUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Friendships", "FromUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.FriendRequests", "ToUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.FriendRequests", "FromUserId", c => c.Int(nullable: false));
        }
    }
}
