namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class report : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReportItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsPost = c.Boolean(nullable: false),
                        ParentId = c.String(),
                        Date = c.DateTime(nullable: false),
                        CommentItem_Id = c.Int(),
                        PostItem_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentItem_Id)
                .ForeignKey("dbo.Posts", t => t.PostItem_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.CommentItem_Id)
                .Index(t => t.PostItem_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReportItems", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReportItems", "PostItem_Id", "dbo.Posts");
            DropForeignKey("dbo.ReportItems", "CommentItem_Id", "dbo.Comments");
            DropIndex("dbo.ReportItems", new[] { "User_Id" });
            DropIndex("dbo.ReportItems", new[] { "PostItem_Id" });
            DropIndex("dbo.ReportItems", new[] { "CommentItem_Id" });
            DropTable("dbo.ReportItems");
        }
    }
}
