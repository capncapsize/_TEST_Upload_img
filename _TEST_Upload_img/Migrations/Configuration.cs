namespace _TEST_Upload_img.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using _TEST_Upload_img.Models;
    using _TEST_Upload_img.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<UploadContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            //ContextKey = "_TEST_Upload_img.DAL.UploadContext";
        }

        protected override void Seed(UploadContext context)
        {
              var images = new List<Image>
              {
                  new Image { Title = "TeTe",     Path = "images/16516356745.jpg" },
                  new Image { Title = "Te",       Path = "images/1364862281846.jpg" },
                  new Image { Title = "Whiskey",  Path = "images/whiskey.gif" }
              };
              images.ForEach(s => context.Images.AddOrUpdate(p => p.ID, s));
              context.SaveChanges();

              var tags = new List<Tag>
            {
                new Tag { Name = "Cup"},
                new Tag { Name = "Liquid"}
            };
              tags.ForEach(s => context.Tags.AddOrUpdate(p => p.Name, s));
              context.SaveChanges();

              var imageTagJoins = new List<ImageTagJoin>
            {
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "TeTe").ID,     TagName = tags.Single(t => t.Name == "Cup").Name},
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "Te").ID,     TagName = tags.Single(t => t.Name == "Cup").Name},
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "Whiskey").ID,     TagName = tags.Single(t => t.Name == "Liquid").Name}
            };

            foreach(ImageTagJoin i in imageTagJoins)
            {
                context.ImageTagJoins.Add(i);
            }
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}