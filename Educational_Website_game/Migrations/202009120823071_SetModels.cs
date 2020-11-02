namespace LibraryDeweyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leaderboards",
                c => new
                    {
                        LeaderboardsID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        TotalMarks = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LeaderboardsID);
            
            CreateTable(
                "dbo.LeaderResults",
                c => new
                    {
                        LeaderResultsID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        LeaderboardsID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LeaderResultsID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Leaderboards", t => t.LeaderboardsID, cascadeDelete: true)
                .Index(t => t.LeaderboardsID)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Results",
                c => new
                    {
                        ResultsID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        LeaderboardsID = c.Int(nullable: false),
                        result = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ResultsID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Leaderboards", t => t.LeaderboardsID, cascadeDelete: true)
                .Index(t => t.LeaderboardsID)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Results", "LeaderboardsID", "dbo.Leaderboards");
            DropForeignKey("dbo.Results", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LeaderResults", "LeaderboardsID", "dbo.Leaderboards");
            DropForeignKey("dbo.LeaderResults", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Results", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Results", new[] { "LeaderboardsID" });
            DropIndex("dbo.LeaderResults", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.LeaderResults", new[] { "LeaderboardsID" });
            DropTable("dbo.Results");
            DropTable("dbo.LeaderResults");
            DropTable("dbo.Leaderboards");
        }
    }
}
