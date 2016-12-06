namespace Ads.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileNameField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ads", "FileName");
        }
    }
}
