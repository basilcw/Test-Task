namespace RIBA_Task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnitQuantities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Quantity1", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Quantity2", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Quantity3", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Quantity4", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Quantity4");
            DropColumn("dbo.Orders", "Quantity3");
            DropColumn("dbo.Orders", "Quantity2");
            DropColumn("dbo.Orders", "Quantity1");
        }
    }
}
