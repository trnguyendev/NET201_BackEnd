using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository Products { get; private set; }
        public IGenericRepository<Category> Categories { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            Categories = new GenericRepository<Category>(_context);
            
        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
    }
}
