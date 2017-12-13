namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        OrderDateTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TotalSum = c.Double(nullable: false),
                        Status = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        PayMethod = c.Int(nullable: false),
                        UserComment = c.Int(),
                        RoleId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.TaskRows",
                c => new
                    {
                        TaskRowId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskRowId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Weight = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        Description = c.String(),
                        Type = c.Int(nullable: false),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.TaskStatusChanges",
                c => new
                    {
                        TaskStatusChangeId = c.Int(nullable: false, identity: true),
                        TaskId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        In = c.DateTime(nullable: false),
                        Out = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TaskStatusChangeId)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TaskId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Patronymic = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Sex = c.String(),
                        Phone = c.String(),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        PasswordConfirm = c.String(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.FeedbackMails",
                c => new
                    {
                        FeedbackMailId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        FeedbackName = c.String(),
                        Body = c.String(),
                        MailDateCreate = c.DateTime(nullable: false),
                        To = c.String(),
                        From = c.String(),
                        ReplyEmail = c.String(),
                        FilePath = c.String(),
                        CustomerId = c.Int(),
                        Customer_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.FeedbackMailId)
                .ForeignKey("dbo.Users", t => t.Customer_UserId)
                .Index(t => t.Customer_UserId);
            
            CreateTable(
                "dbo.OnlineChatMessages",
                c => new
                    {
                        OnlineChatMessageId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ManagerId = c.Int(nullable: false),
                        OnlineChatUniqueId = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Assigned = c.Boolean(nullable: false),
                        IsByManager = c.Boolean(nullable: false),
                        Text = c.String(),
                        IsNew = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OnlineChatMessageId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserComments",
                c => new
                    {
                        UserCommentId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserCommentId)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserRoleId);
            
            CreateTable(
                "dbo.ContactUsers",
                c => new
                    {
                        Contact_ContactId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Contact_ContactId, t.User_UserId })
                .ForeignKey("dbo.Contacts", t => t.Contact_ContactId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Contact_ContactId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.ContactUsers", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserComments", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.TaskStatusChanges", "UserId", "dbo.Users");
            DropForeignKey("dbo.OnlineChatMessages", "UserId", "dbo.Users");
            DropForeignKey("dbo.FeedbackMails", "Customer_UserId", "dbo.Users");
            DropForeignKey("dbo.TaskStatusChanges", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.TaskRows", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.TaskRows", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Tasks", "ContactId", "dbo.Contacts");
            DropIndex("dbo.ContactUsers", new[] { "User_UserId" });
            DropIndex("dbo.ContactUsers", new[] { "Contact_ContactId" });
            DropIndex("dbo.UserComments", new[] { "TaskId" });
            DropIndex("dbo.UserComments", new[] { "UserId" });
            DropIndex("dbo.OnlineChatMessages", new[] { "UserId" });
            DropIndex("dbo.FeedbackMails", new[] { "Customer_UserId" });
            DropIndex("dbo.Users", new[] { "UserRoleId" });
            DropIndex("dbo.TaskStatusChanges", new[] { "UserId" });
            DropIndex("dbo.TaskStatusChanges", new[] { "TaskId" });
            DropIndex("dbo.TaskRows", new[] { "ProductId" });
            DropIndex("dbo.TaskRows", new[] { "TaskId" });
            DropIndex("dbo.Tasks", new[] { "ContactId" });
            DropTable("dbo.ContactUsers");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserComments");
            DropTable("dbo.OnlineChatMessages");
            DropTable("dbo.FeedbackMails");
            DropTable("dbo.Users");
            DropTable("dbo.TaskStatusChanges");
            DropTable("dbo.Products");
            DropTable("dbo.TaskRows");
            DropTable("dbo.Tasks");
            DropTable("dbo.Contacts");
        }
    }
}
