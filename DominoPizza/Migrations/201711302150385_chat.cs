namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class chat : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TaskUsers", newName: "UserTasks");
            DropIndex("dbo.OnlineChatRows", new[] { "CustomerId" });
            DropColumn("dbo.OnlineChatRows", "UserId");
            RenameColumn(table: "dbo.OnlineChatRows", name: "CustomerId", newName: "UserId");
            DropPrimaryKey("dbo.OnlineChatRows");
            DropPrimaryKey("dbo.UserTasks");
            AddColumn("dbo.OnlineChatRows", "OnlineChatRowId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.OnlineChatRows", "OnlineChatRowId");
            AddPrimaryKey("dbo.UserTasks", new[] { "User_UserId", "Task_TaskId" });
            DropColumn("dbo.OnlineChatRows", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OnlineChatRows", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.UserTasks");
            DropPrimaryKey("dbo.OnlineChatRows");
            DropColumn("dbo.OnlineChatRows", "OnlineChatRowId");
            AddPrimaryKey("dbo.UserTasks", new[] { "Task_TaskId", "User_UserId" });
            AddPrimaryKey("dbo.OnlineChatRows", "Id");
            RenameColumn(table: "dbo.OnlineChatRows", name: "UserId", newName: "CustomerId");
            AddColumn("dbo.OnlineChatRows", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.OnlineChatRows", "CustomerId");
            RenameTable(name: "dbo.UserTasks", newName: "TaskUsers");
        }
    }
}
