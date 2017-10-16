namespace PaniDomu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Expenses", "CategoryId");
            AddForeignKey("dbo.Expenses", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            DropColumn("dbo.Expenses", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Expenses", "Category", c => c.String());
            DropForeignKey("dbo.Expenses", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Expenses", new[] { "CategoryId" });
            DropColumn("dbo.Expenses", "Amount");
        }
    }
}
