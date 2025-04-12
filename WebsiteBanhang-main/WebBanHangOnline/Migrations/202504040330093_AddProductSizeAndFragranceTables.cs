namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductSizeAndFragranceTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tb_ProductFragrance",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Fragrance = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.tb_ProductSize",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Size = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tb_Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_ProductSize", "ProductId", "dbo.tb_Product");
            DropForeignKey("dbo.tb_ProductFragrance", "ProductId", "dbo.tb_Product");
            DropIndex("dbo.tb_ProductSize", new[] { "ProductId" });
            DropIndex("dbo.tb_ProductFragrance", new[] { "ProductId" });
            DropTable("dbo.tb_ProductSize");
            DropTable("dbo.tb_ProductFragrance");
        }
    }
}
