namespace _TEST_Upload_img.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        ImageSource = c.String(),
                        Parent_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Category", t => t.Parent_Name)
                .Index(t => t.Parent_Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "Parent_Name", "dbo.Category");
            DropIndex("dbo.Category", new[] { "Parent_Name" });
            DropTable("dbo.Category");
        }
    }
}
