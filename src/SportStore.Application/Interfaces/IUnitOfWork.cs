using SportStore.Domain.Entities;

namespace SportStore.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IGenericRepository<Category> Categories { get; }
        Task<int> CompleteAsync(); //Hàm gọi SaveChanges
    }
}
