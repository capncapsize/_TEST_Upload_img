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
            var categories = new List<Category>
            {
                new Category { Name = "Real Estate", Level = 0},
                new Category { Name = "Vehicles", Level = 0}
            };
            categories.ForEach(s => context.Categories.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            categories = new List<Category>
            {
                new Category { Name = "Apartment", Parent = context.Categories.Single(c => c.Name == "Real Estate"), Level = 1 },
                new Category { Name = "Villa", Parent = context.Categories.Single(c => c.Name == "Real Estate"), Level = 1},
                new Category { Name = "Mansion", Parent = context.Categories.Single(c => c.Name == "Real Estate"), Level = 1 },
                new Category { Name = "Air", Parent = context.Categories.Single(c => c.Name == "Vehicles"), Level = 1 },
                new Category { Name = "Land", Parent = context.Categories.Single(c => c.Name == "Vehicles"), Level = 1 },
                new Category { Name = "Water", Parent = context.Categories.Single(c => c.Name == "Vehicles"), Level = 1 }
            };
            categories.ForEach(s => context.Categories.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            categories = new List<Category>
            {
                new Category { Name = "Urban", Parent = context.Categories.Single(c => c.Name == "Land"), Level = 2 },
                new Category { Name = "Terrain", Parent = context.Categories.Single(c => c.Name == "Land"), Level = 2 }
            };
            categories.ForEach(s => context.Categories.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();


            var images = new List<Image>
              {
                  new Image { Title = "Baia-flash",     Path = "images/732592_baia-flash-48_photo_0_1456915469_1456915470_img.png",                 Category = context.Categories.Single(c => c.Name == "Water")},
                  new Image { Title = "Land Rover 2015",       Path = "images/1372074627.jpg",                                                      Category = context.Categories.Single(c => c.Name == "Terrain")},
                  new Image { Title = "Apartment 6 rooms",  Path = "images/TEST_789.jpg",                                                           Category = context.Categories.Single(c => c.Name == "Apartment")},
                  new Image { Title = "Swedish Turn of the Century Estate", Path = "images/bg_2_2.jpg",                                             Category = context.Categories.Single(c => c.Name == "Mansion")},
                  new Image { Title = "Modern Central New York Apartment", Path = "images/Davis-Seating-Day-view.jpg",                              Category = context.Categories.Single(c => c.Name == "Apartment")},
                  new Image { Title = "Old Industrial New York Apartment", Path = "images/nyc-apartment-for-rent.jpg",                              Category = context.Categories.Single(c => c.Name == "Apartment")},
                  new Image { Title = "Denta Jet", Path = "images/Private-Jet-Dental-Holidays.png",                                                 Category = context.Categories.Single(c => c.Name == "Air")},
                  new Image { Title = "Small speedboat",     Path = "images/search_motor.jpg",                                                      Category = context.Categories.Single(c => c.Name == "Water")},
                  new Image { Title = "Mikhail Starodubov Delta", Path = "images/shutterstock_mikhail_starodubov_delta_private_jets_1949311_b.jpg", Category = context.Categories.Single(c => c.Name == "Air")},
                  new Image { Title = "VW Tiguan 2011",       Path = "images/VW Tiguan.jpg",                                                        Category = context.Categories.Single(c => c.Name == "Urban")},
                  new Image { Title = "VolksWagen Passat 2010",       Path = "images/volkswagen-passat_100528800_m.jpg",                            Category = context.Categories.Single(c => c.Name == "Urban")},
                  new Image { Title = "Audi eTron 2015",       Path = "images/audi-e-tron-car-wallpaper-1280x782.jpg",                              Category = context.Categories.Single(c => c.Name == "Urban")}

              };
            images.ForEach(s => context.Images.AddOrUpdate(p => p.ID, s));
            context.SaveChanges();

            var tags = new List<Tag>
            {
                new Tag { Name = "automatic"},
                new Tag { Name = "volks_wagen"},
                new Tag { Name = "central_park_(new_york)"},
                new Tag { Name = "gated_community"}
            };
            tags.ForEach(s => context.Tags.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var imageTagJoins = new List<ImageTagJoin>
            {
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "Land Rover 2015").ID,     TagName = tags.Single(t => t.Name == "automatic").Name},
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "VW Tiguan 2011").ID,     TagName = tags.Single(t => t.Name == "volks_wagen").Name},
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "VolksWagen Passat 2010").ID,     TagName = tags.Single(t => t.Name == "volks_wagen").Name},
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "VW Tiguan 2011").ID,     TagName = tags.Single(t => t.Name == "automatic").Name},
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "VolksWagen Passat 2010").ID,     TagName = tags.Single(t => t.Name == "automatic").Name},
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "Apartment 6 rooms").ID,     TagName = tags.Single(t => t.Name == "gated_community").Name},
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "Modern Central New York Apartment").ID,     TagName = tags.Single(t => t.Name == "central_park_(new_york)").Name}
            };

            foreach (ImageTagJoin i in imageTagJoins)
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
/*namespace _TEST_Upload_img.Migrations
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
                new Tag { Name = "cup"},
                new Tag { Name = "liquid"}
            };
              tags.ForEach(s => context.Tags.AddOrUpdate(p => p.Name, s));
              context.SaveChanges();

              var imageTagJoins = new List<ImageTagJoin>
            {
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "TeTe").ID,     TagName = tags.Single(t => t.Name == "cup").Name},
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "Te").ID,     TagName = tags.Single(t => t.Name == "cup").Name},
                new ImageTagJoin { ImageID = images.Single(i => i.Title == "Whiskey").ID,     TagName = tags.Single(t => t.Name == "liquid").Name}
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
}*/
