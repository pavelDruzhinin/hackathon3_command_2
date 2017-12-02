namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filepath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeedbackMails", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FeedbackMails", "FilePath");
        }
    }
}
