namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDescriptionAndCreatedBy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MyTasks", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MyTasks", new[] { "CreatedBy_Id" });
            DropColumn("dbo.MyTasks", "Description");
            DropColumn("dbo.MyTasks", "CreatedBy_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyTasks", "CreatedBy_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.MyTasks", "Description", c => c.String());
            CreateIndex("dbo.MyTasks", "CreatedBy_Id");
            AddForeignKey("dbo.MyTasks", "CreatedBy_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
