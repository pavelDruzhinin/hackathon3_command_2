namespace DominoPizza.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles");
            DropIndex("dbo.Users", new[] { "UserRoleId" });
            CreateTable(
                "dbo.UserRoleUsers",
                c => new
                    {
                        UserRole_UserRoleId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRole_UserRoleId, t.User_UserId })
                .ForeignKey("dbo.UserRoles", t => t.UserRole_UserRoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.UserRole_UserRoleId)
                .Index(t => t.User_UserId);
            
            AlterColumn("dbo.Customers", "CustomerFirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "CustomerPatronymic", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "CustomerEmail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoleUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoleUsers", "UserRole_UserRoleId", "dbo.UserRoles");
            DropIndex("dbo.UserRoleUsers", new[] { "User_UserId" });
            DropIndex("dbo.UserRoleUsers", new[] { "UserRole_UserRoleId" });
            AlterColumn("dbo.Customers", "CustomerEmail", c => c.String());
            AlterColumn("dbo.Customers", "CustomerPatronymic", c => c.String());
            AlterColumn("dbo.Customers", "CustomerFirstName", c => c.String());
            DropTable("dbo.UserRoleUsers");
            CreateIndex("dbo.Users", "UserRoleId");
            AddForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles", "UserRoleId", cascadeDelete: true);
        }
    }
}
