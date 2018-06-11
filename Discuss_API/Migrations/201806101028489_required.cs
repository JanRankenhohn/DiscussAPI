namespace Discuss_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "Discussion_Id", "dbo.Discussions");
            DropForeignKey("dbo.Discussions", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Articles", new[] { "Discussion_Id" });
            DropIndex("dbo.Discussions", new[] { "Category_Id" });
            AlterColumn("dbo.Articles", "Discussion_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Discussions", "Category_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Articles", "Discussion_Id");
            CreateIndex("dbo.Discussions", "Category_Id");
            AddForeignKey("dbo.Articles", "Discussion_Id", "dbo.Discussions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Discussions", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Discussions", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Articles", "Discussion_Id", "dbo.Discussions");
            DropIndex("dbo.Discussions", new[] { "Category_Id" });
            DropIndex("dbo.Articles", new[] { "Discussion_Id" });
            AlterColumn("dbo.Discussions", "Category_Id", c => c.Int());
            AlterColumn("dbo.Articles", "Discussion_Id", c => c.Int());
            CreateIndex("dbo.Discussions", "Category_Id");
            CreateIndex("dbo.Articles", "Discussion_Id");
            AddForeignKey("dbo.Discussions", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.Articles", "Discussion_Id", "dbo.Discussions", "Id");
        }
    }
}
