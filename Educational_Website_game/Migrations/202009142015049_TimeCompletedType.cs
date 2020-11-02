namespace LibraryDeweyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeCompletedType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Results", "TimeCompleted", c => c.String());
            DropColumn("dbo.Results", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Results", "Time", c => c.String());
            DropColumn("dbo.Results", "TimeCompleted");
        }
    }
}
