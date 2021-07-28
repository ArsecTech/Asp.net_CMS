namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Admin_Logins : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminLogins",
                c => new
                    {
                        LoginID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 250),
                        Password = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.LoginID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AdminLogins");
        }
    }
}
