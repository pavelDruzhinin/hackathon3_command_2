namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        ContactAddress = c.String(),
                        ContactDateLatestOrder = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerFirstName = c.String(nullable: false),
                        CustomerPatronymic = c.String(nullable: false),
                        CustomerLastName = c.String(),
                        CustomerBirthDate = c.DateTime(nullable: false),
                        CustomerSex = c.String(),
                        CustomerPhone = c.String(),
                        CustomerEmail = c.String(nullable: false),
                        CustomerPassword = c.String(nullable: false),
                        CustomerPasswordConfirm = c.String(nullable: false),
                        CustomerRoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
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
                    })
                .PrimaryKey(t => t.FeedbackMailId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskTotalSum = c.Double(nullable: false),
                        TaskStatus = c.String(),
                        TaskDate = c.DateTime(nullable: false),
                        TaskPayMethod = c.Int(nullable: false),
                        TaskCustomerComment = c.String(),
                        CustomerId = c.Int(nullable: false),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.StatusHistories",
                c => new
                    {
                        StatusHistoryId = c.Int(nullable: false, identity: true),
                        StatusChangedTo = c.String(),
                        StatusChangeTime = c.DateTime(nullable: false),
                        DominosUser_CustomerId = c.Int(nullable: false),
                        ForTask_TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatusHistoryId)
                .ForeignKey("dbo.Customers", t => t.DominosUser_CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.ForTask_TaskId, cascadeDelete: true)
                .Index(t => t.DominosUser_CustomerId)
                .Index(t => t.ForTask_TaskId);
            
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
                        ProductName = c.String(),
                        ProductWeight = c.Double(nullable: false),
                        ProductPrice = c.Double(nullable: false),
                        ProductDescription = c.String(),
                        ProductType = c.Int(nullable: false),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.UserComments",
                c => new
                    {
                        UserCommentId = c.Int(nullable: false, identity: true),
                        UserCommentText = c.String(),
                        UserCommentDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserCommentId)
                .ForeignKey("dbo.Customers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        UserRoleName = c.String(),
                    })
                .PrimaryKey(t => t.UserRoleId);
            
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
                .PrimaryKey(t => t.OnlineChatMessageId);
            
            CreateTable(
                "dbo.CustomerTasks",
                c => new
                    {
                        Customer_CustomerId = c.Int(nullable: false),
                        Task_TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Customer_CustomerId, t.Task_TaskId })
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.Task_TaskId, cascadeDelete: true)
                .Index(t => t.Customer_CustomerId)
                .Index(t => t.Task_TaskId);
            
            CreateTable(
                "dbo.UserRoleCustomers",
                c => new
                    {
                        UserRole_UserRoleId = c.Int(nullable: false),
                        Customer_CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRole_UserRoleId, t.Customer_CustomerId })
                .ForeignKey("dbo.UserRoles", t => t.UserRole_UserRoleId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId, cascadeDelete: true)
                .Index(t => t.UserRole_UserRoleId)
                .Index(t => t.Customer_CustomerId);
            
            CreateTable(
                "dbo.ContactCustomers",
                c => new
                    {
                        Contact_ContactId = c.Int(nullable: false),
                        Customer_CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Contact_ContactId, t.Customer_CustomerId })
                .ForeignKey("dbo.Contacts", t => t.Contact_ContactId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId, cascadeDelete: true)
                .Index(t => t.Contact_ContactId)
                .Index(t => t.Customer_CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContactCustomers", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ContactCustomers", "Contact_ContactId", "dbo.Contacts");
            DropForeignKey("dbo.UserRoleCustomers", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.UserRoleCustomers", "UserRole_UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.CustomerTasks", "Task_TaskId", "dbo.Tasks");
            DropForeignKey("dbo.CustomerTasks", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.UserComments", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.UserComments", "UserId", "dbo.Customers");
            DropForeignKey("dbo.TaskRows", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.TaskRows", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StatusHistories", "ForTask_TaskId", "dbo.Tasks");
            DropForeignKey("dbo.StatusHistories", "DominosUser_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Tasks", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.FeedbackMails", "CustomerId", "dbo.Customers");
            DropIndex("dbo.ContactCustomers", new[] { "Customer_CustomerId" });
            DropIndex("dbo.ContactCustomers", new[] { "Contact_ContactId" });
            DropIndex("dbo.UserRoleCustomers", new[] { "Customer_CustomerId" });
            DropIndex("dbo.UserRoleCustomers", new[] { "UserRole_UserRoleId" });
            DropIndex("dbo.CustomerTasks", new[] { "Task_TaskId" });
            DropIndex("dbo.CustomerTasks", new[] { "Customer_CustomerId" });
            DropIndex("dbo.UserComments", new[] { "TaskId" });
            DropIndex("dbo.UserComments", new[] { "UserId" });
            DropIndex("dbo.TaskRows", new[] { "ProductId" });
            DropIndex("dbo.TaskRows", new[] { "TaskId" });
            DropIndex("dbo.StatusHistories", new[] { "ForTask_TaskId" });
            DropIndex("dbo.StatusHistories", new[] { "DominosUser_CustomerId" });
            DropIndex("dbo.Tasks", new[] { "ContactId" });
            DropIndex("dbo.FeedbackMails", new[] { "CustomerId" });
            DropTable("dbo.ContactCustomers");
            DropTable("dbo.UserRoleCustomers");
            DropTable("dbo.CustomerTasks");
            DropTable("dbo.OnlineChatMessages");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserComments");
            DropTable("dbo.Products");
            DropTable("dbo.TaskRows");
            DropTable("dbo.StatusHistories");
            DropTable("dbo.Tasks");
            DropTable("dbo.FeedbackMails");
            DropTable("dbo.Customers");
            DropTable("dbo.Contacts");
        }
    }
}
