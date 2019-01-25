namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeRatingNotNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Album", "Rating", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Album", "Rating", c => c.Int());
        }
    }
}
