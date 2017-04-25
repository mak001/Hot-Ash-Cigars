namespace HotAshCigars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Images : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        AltText = c.String(),
                        ImageUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Cigars", "Image_ID", c => c.Int());
            CreateIndex("dbo.Cigars", "Image_ID");
            AddForeignKey("dbo.Cigars", "Image_ID", "dbo.Images", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cigars", "Image_ID", "dbo.Images");
            DropIndex("dbo.Cigars", new[] { "Image_ID" });
            DropColumn("dbo.Cigars", "Image_ID");
            DropTable("dbo.Images");
        }
    }
}
