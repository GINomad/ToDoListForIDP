namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OverwriteConvesionsForTasksAndGroups : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MyTasks", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MyTasks", new[] { "CreatedBy_Id" });
            AlterColumn("dbo.Groups", "GroupName", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.MyTasks", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.MyTasks", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.MyTasks", "CreatedBy_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.MyTasks", "CreatedBy_Id");
            AddForeignKey("dbo.MyTasks", "CreatedBy_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyTasks", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MyTasks", new[] { "CreatedBy_Id" });
            AlterColumn("dbo.MyTasks", "CreatedBy_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.MyTasks", "Description", c => c.String());
            AlterColumn("dbo.MyTasks", "Title", c => c.String());
            AlterColumn("dbo.Groups", "GroupName", c => c.String());
            CreateIndex("dbo.MyTasks", "CreatedBy_Id");
            AddForeignKey("dbo.MyTasks", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
