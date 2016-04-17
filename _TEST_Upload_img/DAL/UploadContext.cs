using _TEST_Upload_img.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions; //Install-Package EntityFramework

namespace _TEST_Upload_img.DAL
{
    public class UploadContext : DbContext
    {
        public UploadContext()
            : base("UploadContext")
        {
           //this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Image> Images { get; set; }
        public DbSet<ImageTagJoin> ImageTagJoins { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //Remove pluralized table names
        }
    }
}