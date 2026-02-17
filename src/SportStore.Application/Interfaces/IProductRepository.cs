using SportStore.Application.DTOs;
using SportStore.Application.Interfaces;
using SportStore.Domain.Entities;

namespace SportStore.Application.Interfaces
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<IEnumerable<ProductHomeDto>> GetHomeProductsAsync();
    }
}
