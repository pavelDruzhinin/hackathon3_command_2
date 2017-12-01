namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedbackmail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeedbackMails",
                c => new
                    {
                        FeedbackMailId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        From = c.String(),
                        Body = c.String(),
                        MailDateCreate = c.DateTime(nullable: false),
                        To = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeedbackMailId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Users", "UserSex", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "UserBirthDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Users", "Login");
            DropColumn("dbo.Users", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Password", c => c.String());
            AddColumn("dbo.Users", "Login", c => c.String());
            DropForeignKey("dbo.FeedbackMails", "UserId", "dbo.Users");
            DropIndex("dbo.FeedbackMails", new[] { "UserId" });
            DropColumn("dbo.Users", "UserBirthDate");
            DropColumn("dbo.Users", "UserSex");
            DropTable("dbo.FeedbackMails");
        }
    }
}
