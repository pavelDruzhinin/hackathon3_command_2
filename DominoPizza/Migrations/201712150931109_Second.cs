namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CustomerTasks", newName: "CustomerTask1");
            CreateTable(
                "dbo.CustomerTasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId);
            
            CreateTable(
                "dbo.CustomerTaskTasks",
                c => new
                    {
                        CustomerTask_TaskId = c.Int(nullable: false),
                        Task_TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerTask_TaskId, t.Task_TaskId })
                .ForeignKey("dbo.CustomerTasks", t => t.CustomerTask_TaskId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.Task_TaskId, cascadeDelete: true)
                .Index(t => t.CustomerTask_TaskId)
                .Index(t => t.Task_TaskId);
            
            AddColumn("dbo.Customers", "CustomerTask_TaskId", c => c.Int());
            CreateIndex("dbo.Customers", "CustomerTask_TaskId");
            AddForeignKey("dbo.Customers", "CustomerTask_TaskId", "dbo.CustomerTasks", "TaskId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerTaskTasks", "Task_TaskId", "dbo.Tasks");
            DropForeignKey("dbo.CustomerTaskTasks", "CustomerTask_TaskId", "dbo.CustomerTasks");
            DropForeignKey("dbo.Customers", "CustomerTask_TaskId", "dbo.CustomerTasks");
            DropIndex("dbo.CustomerTaskTasks", new[] { "Task_TaskId" });
            DropIndex("dbo.CustomerTaskTasks", new[] { "CustomerTask_TaskId" });
            DropIndex("dbo.Customers", new[] { "CustomerTask_TaskId" });
            DropColumn("dbo.Customers", "CustomerTask_TaskId");
            DropTable("dbo.CustomerTaskTasks");
            DropTable("dbo.CustomerTasks");
            RenameTable(name: "dbo.CustomerTask1", newName: "CustomerTasks");
        }
    }
}
