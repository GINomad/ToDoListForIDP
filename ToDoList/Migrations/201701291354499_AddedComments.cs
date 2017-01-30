namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        TaskId = c.Int(nullable: false),
                        TimePosted = c.DateTime(nullable: false),
                        Task_MyTaskId = c.Int(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.MyTasks", t => t.Task_MyTaskId)
                .Index(t => t.Task_MyTaskId);
            
            AddColumn("dbo.MyTasks", "GroupId", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Task_MyTaskId", "dbo.MyTasks");
            DropIndex("dbo.Comments", new[] { "Task_MyTaskId" });
            DropColumn("dbo.MyTasks", "GroupId");
            DropTable("dbo.Comments");
        }
    }
}
