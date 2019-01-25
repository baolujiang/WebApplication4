namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeschema : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "MR.Album", newSchema: "dbo");
            MoveTable(name: "HR.Artist", newSchema: "dbo");
            MoveTable(name: "HR.ArtistDetail", newSchema: "dbo");
        }
        
        public override void Down()
        {
            MoveTable(name: "dbo.ArtistDetail", newSchema: "HR");
            MoveTable(name: "dbo.Artist", newSchema: "HR");
            MoveTable(name: "dbo.Album", newSchema: "MR");
        }
    }
}
