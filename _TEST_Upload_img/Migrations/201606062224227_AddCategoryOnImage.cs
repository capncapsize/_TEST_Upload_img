namespace _TEST_Upload_img.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryOnImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Image", "Category_Name", c => c.String(maxLength: 128));
            CreateIndex("dbo.Image", "Category_Name");
            AddForeignKey("dbo.Image", "Category_Name", "dbo.Category", "Name");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Image", "Category_Name", "dbo.Category");
            DropIndex("dbo.Image", new[] { "Category_Name" });
            DropColumn("dbo.Image", "Category_Name");
        }
    }
}
