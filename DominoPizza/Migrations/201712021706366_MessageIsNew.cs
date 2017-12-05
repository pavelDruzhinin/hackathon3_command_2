namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageIsNew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OnlineChatRows", "MessageIsNew", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OnlineChatRows", "MessageIsNew");
        }
    }
}
