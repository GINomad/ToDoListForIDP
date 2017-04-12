namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveForeighKeyFromTasks : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MyTasks", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyTasks", "ApplicationUserId", c => c.String());
        }
    }
}
