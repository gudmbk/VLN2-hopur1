namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repp5 : DbMigration
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
                        ParentId = c.String(),
                        ReportedById = c.String(),
                        Date = c.DateTime(nullable: false),
                        CommentItem_Id = c.Int(),
                        PostItem_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ReportItems", "User_Id");
            CreateIndex("dbo.ReportItems", "PostItem_Id");
            CreateIndex("dbo.ReportItems", "CommentItem_Id");
            AddForeignKey("dbo.ReportItems", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ReportItems", "PostItem_Id", "dbo.Posts", "Id");
            AddForeignKey("dbo.ReportItems", "CommentItem_Id", "dbo.Comments", "Id");
        }
    }
}
