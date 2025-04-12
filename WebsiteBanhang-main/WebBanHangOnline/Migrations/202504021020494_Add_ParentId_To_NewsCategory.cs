namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ParentId_To_NewsCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_NewsCategory", "ParentId", c => c.Int());
            CreateIndex("dbo.tb_NewsCategory", "ParentId");
            AddForeignKey("dbo.tb_NewsCategory", "ParentId", "dbo.tb_NewsCategory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_NewsCategory", "ParentId", "dbo.tb_NewsCategory");
            DropIndex("dbo.tb_NewsCategory", new[] { "ParentId" });
            DropColumn("dbo.tb_NewsCategory", "ParentId");
        }
    }
}
