using Microsoft.EntityFrameworkCore;
using SportStore.Application.Interfaces;
using System.Runtime.CompilerServices;

namespace SportStore.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();
        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public IQueryable<T> GetQueryable()
        {
            // Hàm này cho phép trả về bộ khung IQueryable 
            // để tầng Service có thể .Where(), .Skip(), .Take() thoải mái
            // trước khi thực sự biến thành câu lệnh SQL chạy xuống DB.
            return _context.Set<T>().AsQueryable();
        }
    } 
}
