namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCloseStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MyTasks", "Closed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MyTasks", "Closed");
        }
    }
}
