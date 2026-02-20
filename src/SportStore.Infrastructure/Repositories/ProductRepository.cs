using Microsoft.EntityFrameworkCore;
using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<ProductHomeDto>> GetHomeProductsAsync()
        {
            return await _context.Products
                .Where(p => p.IsActive)
                .Select(p => new ProductHomeDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    BasePrice = p.BasePrice,
                    Thumbnail = p.Thumbnail,
                })
                .ToListAsync();
        }
    }
}
