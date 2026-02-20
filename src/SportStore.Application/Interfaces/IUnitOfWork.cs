using SportStore.Domain.Entities;

namespace SportStore.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Brand> Brands { get; }
        IGenericRepository<ProductColor> ProductColors { get; }
        IGenericRepository<ProductSize> ProductSizes { get; }
        IGenericRepository<ProductVariant> ProductVariants { get; }
        IGenericRepository<ProductImage> ProductImages { get; }
       IGenericRepository<Order> Orders { get; }
        Task<int> CompleteAsync(); //Hàm gọi SaveChanges
    }
}
