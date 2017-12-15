namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "CustomerTask_TaskId", "dbo.CustomerTasks");
            DropIndex("dbo.Customers", new[] { "CustomerTask_TaskId" });
            DropColumn("dbo.Customers", "CustomerTask_TaskId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "CustomerTask_TaskId", c => c.Int());
            CreateIndex("dbo.Customers", "CustomerTask_TaskId");
            AddForeignKey("dbo.Customers", "CustomerTask_TaskId", "dbo.CustomerTasks", "TaskId");
        }
    }
}
