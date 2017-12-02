namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedbackс : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeedbackMails",
                c => new
                    {
                        FeedbackMailId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Body = c.String(),
                        MailDateCreate = c.DateTime(nullable: false),
                        To = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeedbackMailId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            DropColumn("dbo.Users", "UserSex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserSex", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.FeedbackMails", "CustomerId", "dbo.Customers");
            DropIndex("dbo.FeedbackMails", new[] { "CustomerId" });
            DropTable("dbo.FeedbackMails");
        }
    }
}
