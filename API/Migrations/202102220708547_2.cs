namespace API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Tb_M_Account");
            AlterColumn("dbo.Tb_M_Account", "AccountID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Tb_M_Account", "AccountID");
            CreateIndex("dbo.Tb_M_Account", "AccountID");
            AddForeignKey("dbo.Tb_M_Account", "AccountID", "dbo.Tb_M_Employee", "EmployeeID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tb_M_Account", "AccountID", "dbo.Tb_M_Employee");
            DropIndex("dbo.Tb_M_Account", new[] { "AccountID" });
            DropPrimaryKey("dbo.Tb_M_Account");
            AlterColumn("dbo.Tb_M_Account", "AccountID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Tb_M_Account", "AccountID");
        }
    }
}
