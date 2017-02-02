namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedForeighKeyComment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Task_MyTaskId", "dbo.MyTasks");
            DropIndex("dbo.Comments", new[] { "Task_MyTaskId" });
            RenameColumn(table: "dbo.Comments", name: "Task_MyTaskId", newName: "MyTaskId");
            AlterColumn("dbo.Comments", "MyTaskId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "MyTaskId");
            AddForeignKey("dbo.Comments", "MyTaskId", "dbo.MyTasks", "MyTaskId", cascadeDelete: true);
            DropColumn("dbo.Comments", "TaskId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "TaskId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "MyTaskId", "dbo.MyTasks");
            DropIndex("dbo.Comments", new[] { "MyTaskId" });
            AlterColumn("dbo.Comments", "MyTaskId", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "MyTaskId", newName: "Task_MyTaskId");
            CreateIndex("dbo.Comments", "Task_MyTaskId");
            AddForeignKey("dbo.Comments", "Task_MyTaskId", "dbo.MyTasks", "MyTaskId");
        }
    }
}
