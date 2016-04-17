namespace _TEST_Upload_img.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ImageTagJoin",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageID = c.Int(nullable: false),
                        TagName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Image", t => t.ImageID, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagName)
                .Index(t => new { t.ImageID, t.TagName }, unique: true, name: "IX_ImageAndTag");
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageTagJoin", "TagName", "dbo.Tag");
            DropForeignKey("dbo.ImageTagJoin", "ImageID", "dbo.Image");
            DropIndex("dbo.ImageTagJoin", "IX_ImageAndTag");
            DropTable("dbo.Tag");
            DropTable("dbo.ImageTagJoin");
            DropTable("dbo.Image");
        }
    }
}
