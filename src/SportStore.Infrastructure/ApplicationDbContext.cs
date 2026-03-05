using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportStore.Domain.Entities;
using System.Reflection.Emit;

namespace SportStore.Infrastructure
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // BẮT BUỘC PHẢI CÓ DÒNG NÀY ĐỂ MAPPING IDENTITY TABLES
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductSize>()
                .HasOne(ps => ps.Category)
                .WithMany(c => c.Sizes)
                .HasForeignKey(ps => ps.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
