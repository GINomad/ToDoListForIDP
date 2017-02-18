namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserToTasks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyTasks", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.MyTasks", "ApplicationUserId");
            AddForeignKey("dbo.MyTasks", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyTasks", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.MyTasks", new[] { "ApplicationUserId" });
            DropColumn("dbo.MyTasks", "ApplicationUserId");
        }
    }
}
