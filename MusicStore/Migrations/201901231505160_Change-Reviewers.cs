namespace MusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeReviewers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviewer", "Name", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviewer", "Name", c => c.String());
        }
    }
}
