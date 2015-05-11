namespace klukk_social.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bla : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Comments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(),
                        PosterName = c.String(),
                        Body = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
