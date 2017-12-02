namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CustomerPassword", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "CustomerPasswordConfirm", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "CustomerPasswordConfirm");
            DropColumn("dbo.Customers", "CustomerPassword");
        }
    }
}
