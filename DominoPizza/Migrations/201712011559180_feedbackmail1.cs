namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedbackmail1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.FeedbackMails", "From");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FeedbackMails", "From", c => c.String());
        }
    }
}
