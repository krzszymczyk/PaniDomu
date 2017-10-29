namespace PaniDomu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDetailsToExpense : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expenses", "Details", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Expenses", "Details");
        }
    }
}
