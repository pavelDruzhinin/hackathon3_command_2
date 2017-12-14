namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class history : DbMigration
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
                        DominosUser_CustomerId = c.Int(nullable: false),
                        ForTask_TaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StatusHistoryId)
                .ForeignKey("dbo.Customers", t => t.DominosUser_CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Tasks", t => t.ForTask_TaskId, cascadeDelete: true)
                .Index(t => t.DominosUser_CustomerId)
                .Index(t => t.ForTask_TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatusHistories", "ForTask_TaskId", "dbo.Tasks");
            DropForeignKey("dbo.StatusHistories", "DominosUser_CustomerId", "dbo.Customers");
            DropIndex("dbo.StatusHistories", new[] { "ForTask_TaskId" });
            DropIndex("dbo.StatusHistories", new[] { "DominosUser_CustomerId" });
            DropTable("dbo.StatusHistories");
        }
    }
}
