namespace WebBanHangOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFragranceOrderDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_OrderDetail", "Fragrance", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_OrderDetail", "Fragrance");
        }
    }
}
