namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeedbackMails", "ReplyEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FeedbackMails", "ReplyEmail");
        }
    }
}
