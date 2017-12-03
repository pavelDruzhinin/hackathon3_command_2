namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRoleUsers", "UserRole_UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserRoleUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.UserComments", "UserId", "dbo.Users");
            DropForeignKey("dbo.TaskUsers", "Task_TaskId", "dbo.Tasks");
            DropForeignKey("dbo.TaskUsers", "User_UserId", "dbo.Users");
            DropIndex("dbo.UserRoleUsers", new[] { "UserRole_UserRoleId" });
            DropIndex("dbo.UserRoleUsers", new[] { "User_UserId" });
            DropIndex("dbo.TaskUsers", new[] { "Task_TaskId" });
            DropIndex("dbo.TaskUsers", new[] { "User_UserId" });
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
            
            AddColumn("dbo.Customers", "CustomerRoleId", c => c.Int(nullable: false));
            AddForeignKey("dbo.UserComments", "UserId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            DropTable("dbo.Users");
            DropTable("dbo.UserRoleUsers");
            DropTable("dbo.TaskUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TaskUsers",
                c => new
                    {
                        Task_TaskId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Task_TaskId, t.User_UserId });
            
            CreateTable(
                "dbo.UserRoleUsers",
                c => new
                    {
                        UserRole_UserRoleId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRole_UserRoleId, t.User_UserId });
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        UserFirstName = c.String(),
                        UserPatronymic = c.String(),
                        UserLastName = c.String(),
                        UserRoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            DropForeignKey("dbo.UserRoleCustomers", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.UserRoleCustomers", "UserRole_UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.CustomerTasks", "Task_TaskId", "dbo.Tasks");
            DropForeignKey("dbo.CustomerTasks", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.UserComments", "UserId", "dbo.Customers");
            DropIndex("dbo.UserRoleCustomers", new[] { "Customer_CustomerId" });
            DropIndex("dbo.UserRoleCustomers", new[] { "UserRole_UserRoleId" });
            DropIndex("dbo.CustomerTasks", new[] { "Task_TaskId" });
            DropIndex("dbo.CustomerTasks", new[] { "Customer_CustomerId" });
            DropColumn("dbo.Customers", "CustomerRoleId");
            DropTable("dbo.UserRoleCustomers");
            DropTable("dbo.CustomerTasks");
            CreateIndex("dbo.TaskUsers", "User_UserId");
            CreateIndex("dbo.TaskUsers", "Task_TaskId");
            CreateIndex("dbo.UserRoleUsers", "User_UserId");
            CreateIndex("dbo.UserRoleUsers", "UserRole_UserRoleId");
            AddForeignKey("dbo.TaskUsers", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.TaskUsers", "Task_TaskId", "dbo.Tasks", "TaskId", cascadeDelete: true);
            AddForeignKey("dbo.UserComments", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UserRoleUsers", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UserRoleUsers", "UserRole_UserRoleId", "dbo.UserRoles", "UserRoleId", cascadeDelete: true);
        }
    }
}
