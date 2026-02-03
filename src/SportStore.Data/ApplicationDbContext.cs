using Microsoft.EntityFrameworkCore;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColor { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }

        // Phần này để đảm bảo 1 sản phẩm không bị trùng lặp biến thể 
        // (Ví dụ: Không thể add 2 dòng cùng là Áo A - Màu Đỏ - Size M)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình Unique Index cho biến thể
            // Một sản phẩm + Một màu + Một size => Phải là duy nhất
            modelBuilder.Entity<ProductVariant>()
                .HasIndex(v => new { v.ProductId, v.ColorId, v.SizeId })
                .IsUnique();
        }
    }
}
