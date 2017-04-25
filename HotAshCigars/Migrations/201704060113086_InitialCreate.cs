namespace HotAshCigars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cigars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Weight = c.Single(nullable: false),
                        Dimensions_Width = c.Single(nullable: false),
                        Dimensions_Height = c.Single(nullable: false),
                        Dimensions_Depth = c.Single(nullable: false),
                        Price = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        HowPaid = c.String(),
                        DiscountAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        PricePaidEach = c.Double(nullable: false),
                        Cigar_ID = c.Int(nullable: false),
                        Order_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cigars", t => t.Cigar_ID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .Index(t => t.Cigar_ID)
                .Index(t => t.Order_ID);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        Quantity = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        Cigar_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cigars", t => t.Cigar_ID, cascadeDelete: true)
                .Index(t => t.Cigar_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCarts", "Cigar_ID", "dbo.Cigars");
            DropForeignKey("dbo.OrderDetails", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "Cigar_ID", "dbo.Cigars");
            DropIndex("dbo.ShoppingCarts", new[] { "Cigar_ID" });
            DropIndex("dbo.OrderDetails", new[] { "Order_ID" });
            DropIndex("dbo.OrderDetails", new[] { "Cigar_ID" });
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.Cigars");
        }
    }
}
