using Microsoft.EntityFrameworkCore;
using ProductsWebApi.Dal;

namespace ProductsWebApi
{
  public class ProductDbContext : DbContext
  {

    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    public DbSet<Store> Stores { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      modelBuilder.Entity<Product>()
        .Property(x => x.Name)
        .IsRequired()
        ;

      modelBuilder.Entity<Product>()
          .HasOne(p => p.Store)
          .WithMany(s => s.Products)
          .HasForeignKey(p => p.StoreId)
          .IsRequired()
          ;
    }


  }
}
