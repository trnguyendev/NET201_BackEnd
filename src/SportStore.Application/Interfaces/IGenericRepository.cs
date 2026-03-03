using SportStore.Application.DTOs;

namespace SportStore.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> GetQueryable();
        Task<PageResult<T>> GetPagedAsync(IQueryable<T> query, int pageNumber, int pageSize);
    }
}
