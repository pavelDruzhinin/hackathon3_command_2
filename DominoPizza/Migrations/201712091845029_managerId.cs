namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class managerId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OnlineChatMessages", "ManagerId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OnlineChatMessages", "ManagerId");
        }
    }
}
