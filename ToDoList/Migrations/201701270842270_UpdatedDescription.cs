namespace ToDoList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MyTasks", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MyTasks", "Description", c => c.String(nullable: false));
        }
    }
}
