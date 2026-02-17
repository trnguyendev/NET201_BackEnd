using AutoMapper;
using SportStore.Application.DTOs;
using SportStore.Domain.Entities;

namespace SportStore.Application.Mappings
{
    public class ProductMappingProfile:  Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
