using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context): base(context) { }
    }
}
