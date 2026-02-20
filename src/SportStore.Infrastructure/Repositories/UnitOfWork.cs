using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Products { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public IGenericRepository<Brand> Brands { get; private set; }
        public IGenericRepository<ProductColor> ProductColors { get; private set; }
        public IGenericRepository<ProductSize> ProductSizes { get; private set; }
        public IGenericRepository<ProductVariant> ProductVariants { get; private set; }
        public IGenericRepository<ProductImage> ProductImages { get; private set; }
        public IGenericRepository<Order> Orders { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Categories = new GenericRepository<Category>(_context);
            Brands = new GenericRepository<Brand>(_context);
            ProductColors = new GenericRepository<ProductColor>(_context);
            ProductSizes = new GenericRepository<ProductSize>(_context);
            ProductVariants = new GenericRepository<ProductVariant>(_context);
            Orders = new GenericRepository<Order>(_context);
        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
