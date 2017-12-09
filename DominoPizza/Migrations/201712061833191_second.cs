namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomerFirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "CustomerPatronymic", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "CustomerEmail", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "CustomerPassword", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "CustomerPasswordConfirm", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CustomerPasswordConfirm", c => c.String());
            AlterColumn("dbo.Customers", "CustomerPassword", c => c.String());
            AlterColumn("dbo.Customers", "CustomerEmail", c => c.String());
            AlterColumn("dbo.Customers", "CustomerPatronymic", c => c.String());
            AlterColumn("dbo.Customers", "CustomerFirstName", c => c.String());
        }
    }
}
