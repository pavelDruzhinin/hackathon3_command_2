namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusHistories",
                c => new
                    {
                        StatusHistoryId = c.Int(nullable: false, identity: true),
                        StatusChangedTo = c.String(),
                        StatusChangeTime = c.DateTime(nullable: false),
                        Changer_CustomerId = c.Int(nullable: false),
                        ForTask_TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatusHistoryId)
                .ForeignKey("dbo.Customers", t => t.Changer_CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.ForTask_TaskId, cascadeDelete: true)
                .Index(t => t.Changer_CustomerId)
                .Index(t => t.ForTask_TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatusHistories", "ForTask_TaskId", "dbo.Tasks");
            DropForeignKey("dbo.StatusHistories", "Changer_CustomerId", "dbo.Customers");
            DropIndex("dbo.StatusHistories", new[] { "ForTask_TaskId" });
            DropIndex("dbo.StatusHistories", new[] { "Changer_CustomerId" });
            DropTable("dbo.StatusHistories");
        }
    }
}
