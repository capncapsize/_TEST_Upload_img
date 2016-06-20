namespace _TEST_Upload_img.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAttributeOnCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Category", "Parent_Name", "dbo.Category");
            DropPrimaryKey("dbo.Category");
            AlterColumn("dbo.Category", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Category", "Name");
            AddForeignKey("dbo.Category", "Parent_Name", "dbo.Category", "Name");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "Parent_Name", "dbo.Category");
            DropPrimaryKey("dbo.Category");
            AlterColumn("dbo.Category", "Name", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Category", "Name");
            AddForeignKey("dbo.Category", "Parent_Name", "dbo.Category", "Name");
        }
    }
}
