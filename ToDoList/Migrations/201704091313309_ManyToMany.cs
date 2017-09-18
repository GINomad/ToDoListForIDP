namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MyTasks", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.MyTasks", new[] { "ApplicationUserId" });
            CreateTable(
                "dbo.ApplicationUserMyTasks",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        MyTask_MyTaskId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.MyTask_MyTaskId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.MyTasks", t => t.MyTask_MyTaskId, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.MyTask_MyTaskId);
            
            AlterColumn("dbo.MyTasks", "ApplicationUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserMyTasks", "MyTask_MyTaskId", "dbo.MyTasks");
            DropForeignKey("dbo.ApplicationUserMyTasks", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserMyTasks", new[] { "MyTask_MyTaskId" });
            DropIndex("dbo.ApplicationUserMyTasks", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.MyTasks", "ApplicationUserId", c => c.String(maxLength: 128));
            DropTable("dbo.ApplicationUserMyTasks");
            CreateIndex("dbo.MyTasks", "ApplicationUserId");
            AddForeignKey("dbo.MyTasks", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
