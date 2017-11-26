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
                        CustomerFirstName = c.String(),
                        CustomerPatronymic = c.String(),
                        CustomerLastName = c.String(),
                        CustomerBirthDate = c.DateTime(nullable: false),
                        CustomerSex = c.Boolean(nullable: false),
                        CustomerPhone = c.String(),
                        CustomerEmail = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
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
                .ForeignKey("dbo.Tasks", t => t.TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserFirstName = c.String(),
                        UserPatronymic = c.String(),
                        UserLastName = c.String(),
                        UserSex = c.Boolean(nullable: false),
                        UserBirthDate = c.DateTime(nullable: false),
                        UserRoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        UserRoleName = c.String(),
                    })
                .PrimaryKey(t => t.UserRoleId);
            
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
            
            CreateTable(
                "dbo.TaskUsers",
                c => new
                    {
                        Task_TaskId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Task_TaskId, t.User_UserId })
                .ForeignKey("dbo.Tasks", t => t.Task_TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Task_TaskId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.TaskUsers", "Task_TaskId", "dbo.Tasks");
            DropForeignKey("dbo.UserComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserComments", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.TaskRows", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.TaskRows", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Tasks", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.ContactCustomers", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ContactCustomers", "Contact_ContactId", "dbo.Contacts");
            DropIndex("dbo.TaskUsers", new[] { "User_UserId" });
            DropIndex("dbo.TaskUsers", new[] { "Task_TaskId" });
            DropIndex("dbo.ContactCustomers", new[] { "Customer_CustomerId" });
            DropIndex("dbo.ContactCustomers", new[] { "Contact_ContactId" });
            DropIndex("dbo.Users", new[] { "UserRoleId" });
            DropIndex("dbo.UserComments", new[] { "TaskId" });
            DropIndex("dbo.UserComments", new[] { "UserId" });
            DropIndex("dbo.TaskRows", new[] { "ProductId" });
            DropIndex("dbo.TaskRows", new[] { "TaskId" });
            DropIndex("dbo.Tasks", new[] { "ContactId" });
            DropTable("dbo.TaskUsers");
            DropTable("dbo.ContactCustomers");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.UserComments");
            DropTable("dbo.Products");
            DropTable("dbo.TaskRows");
            DropTable("dbo.Tasks");
            DropTable("dbo.Customers");
            DropTable("dbo.Contacts");
        }
    }
}
