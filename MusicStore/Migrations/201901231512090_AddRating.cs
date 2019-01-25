namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Album", "Rating", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Album", "Rating");
        }
    }
}
