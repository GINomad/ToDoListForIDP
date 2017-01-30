namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTasksAndGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Byte(nullable: false),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.MyTasks",
                c => new
                    {
                        MyTaskId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Created = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        TaskPriority = c.Int(nullable: false),
                        CreatedBy_Id = c.String(maxLength: 128),
                        Group_GroupId = c.Byte(),
                    })
                .PrimaryKey(t => t.MyTaskId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.Groups", t => t.Group_GroupId)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Group_GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyTasks", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.MyTasks", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MyTasks", new[] { "Group_GroupId" });
            DropIndex("dbo.MyTasks", new[] { "CreatedBy_Id" });
            DropTable("dbo.MyTasks");
            DropTable("dbo.Groups");
        }
    }
}
