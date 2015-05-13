namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class solo : DbMigration
    {
        public override void Up()
        {

        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReportItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsPost = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        CommentItem_Id = c.Int(),
                        PostItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ReportItems", "PostItem_Id");
            CreateIndex("dbo.ReportItems", "CommentItem_Id");
            CreateIndex("dbo.ReportItems", "UserId");
            AddForeignKey("dbo.ReportItems", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ReportItems", "PostItem_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.ReportItems", "CommentItem_Id", "dbo.Comments", "Id");
        }
    }
}
