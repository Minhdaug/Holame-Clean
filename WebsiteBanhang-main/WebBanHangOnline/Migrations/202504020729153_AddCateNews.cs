namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCateNews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_NewsCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Icon = c.String(maxLength: 250),
                        SeoTitle = c.String(maxLength: 250),
                        SeoDescription = c.String(maxLength: 500),
                        SeoKeywords = c.String(maxLength: 250),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        Modifiedby = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tb_News", "NewsCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_News", "NewsCategoryId");
            AddForeignKey("dbo.tb_News", "NewsCategoryId", "dbo.tb_NewsCategory", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_News", "NewsCategoryId", "dbo.tb_NewsCategory");
            DropIndex("dbo.tb_News", new[] { "NewsCategoryId" });
            DropColumn("dbo.tb_News", "NewsCategoryId");
            DropTable("dbo.tb_NewsCategory");
        }
    }
}
