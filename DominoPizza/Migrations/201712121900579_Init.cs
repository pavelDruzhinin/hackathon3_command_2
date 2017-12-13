namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomerBirthDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "CustomerSex", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CustomerSex", c => c.String());
            AlterColumn("dbo.Customers", "CustomerBirthDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
