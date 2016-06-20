namespace _TEST_Upload_img.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryLevelAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "Level", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "Level");
        }
    }
}
