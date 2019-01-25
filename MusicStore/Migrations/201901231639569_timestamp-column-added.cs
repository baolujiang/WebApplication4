namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timestampcolumnadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artist", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Artist", "RowVersion");
        }
    }
}
