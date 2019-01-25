namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "MR.Album",
                c => new
                    {
                        AlbumID = c.Guid(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        ArtistID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumID)
                .ForeignKey("HR.Artist", t => t.ArtistID, cascadeDelete: true)
                .Index(t => t.ArtistID);
            
            CreateTable(
                "HR.Artist",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Instrument = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ArtistId);
            
            CreateTable(
                "HR.ArtistDetail",
                c => new
                    {
                        ArtistID = c.Int(nullable: false),
                        Bio = c.String(),
                    })
                .PrimaryKey(t => t.ArtistID)
                .ForeignKey("HR.Artist", t => t.ArtistID)
                .Index(t => t.ArtistID);
            
            CreateTable(
                "dbo.Reviewer",
                c => new
                    {
                        ReviewerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ReviewerID);
            
            CreateTable(
                "dbo.ReviewerAlbums",
                c => new
                    {
                        Reviewer_ReviewerID = c.Int(nullable: false),
                        Album_AlbumID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Reviewer_ReviewerID, t.Album_AlbumID })
                .ForeignKey("dbo.Reviewer", t => t.Reviewer_ReviewerID, cascadeDelete: true)
                .ForeignKey("MR.Album", t => t.Album_AlbumID, cascadeDelete: true)
                .Index(t => t.Reviewer_ReviewerID)
                .Index(t => t.Album_AlbumID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReviewerAlbums", "Album_AlbumID", "MR.Album");
            DropForeignKey("dbo.ReviewerAlbums", "Reviewer_ReviewerID", "dbo.Reviewer");
            DropForeignKey("HR.ArtistDetail", "ArtistID", "HR.Artist");
            DropForeignKey("MR.Album", "ArtistID", "HR.Artist");
            DropIndex("dbo.ReviewerAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.ReviewerAlbums", new[] { "Reviewer_ReviewerID" });
            DropIndex("HR.ArtistDetail", new[] { "ArtistID" });
            DropIndex("MR.Album", new[] { "ArtistID" });
            DropTable("dbo.ReviewerAlbums");
            DropTable("dbo.Reviewer");
            DropTable("HR.ArtistDetail");
            DropTable("HR.Artist");
            DropTable("MR.Album");
        }
    }
}
