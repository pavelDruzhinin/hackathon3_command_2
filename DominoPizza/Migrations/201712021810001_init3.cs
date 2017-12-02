namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeedbackMails", "CustomerId", c => c.Int());
            CreateIndex("dbo.FeedbackMails", "CustomerId");
            AddForeignKey("dbo.FeedbackMails", "CustomerId", "dbo.Customers", "CustomerId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeedbackMails", "CustomerId", "dbo.Customers");
            DropIndex("dbo.FeedbackMails", new[] { "CustomerId" });
            DropColumn("dbo.FeedbackMails", "CustomerId");
        }
    }
}
