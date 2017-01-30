namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEstimatedTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyTasks", "TimeEstimated", c => c.Single(nullable: false));
            DropColumn("dbo.MyTasks", "Created");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyTasks", "Created", c => c.DateTime(nullable: false));
            DropColumn("dbo.MyTasks", "TimeEstimated");
        }
    }
}
