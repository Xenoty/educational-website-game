namespace LibraryDeweyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedResultodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Results", "Time", c => c.String());
            AddColumn("dbo.Results", "DateCompleted", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Results", "DateCompleted");
            DropColumn("dbo.Results", "Time");
        }
    }
}
