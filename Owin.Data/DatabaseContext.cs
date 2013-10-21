using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Owin.Domain.Entities;

namespace Owin.Data
{
    public class DatabaseContext : DbContext
    {


      public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DatabaseContext() { }
        public DatabaseContext(string connectionString) : base(connectionString) { }
    }

    internal class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("items");
            Property(p => p.Id).HasColumnName("ID");
            HasKey(p => p.Id);

            Property(p => p.Code).HasColumnName("name");

            Property(p => p.Price).HasColumnName("price");
            Ignore(p => p.OldPrice);
        }
    }
}
