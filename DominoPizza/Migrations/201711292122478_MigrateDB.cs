namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Login", c => c.String());
            AddColumn("dbo.Users", "Password", c => c.String());
            DropColumn("dbo.Users", "UserSex");
            DropColumn("dbo.Users", "UserBirthDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserBirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "UserSex", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Login");
        }
    }
}
