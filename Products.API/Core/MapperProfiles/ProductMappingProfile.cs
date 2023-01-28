using AutoMapper;
using Products.API.Core.DTOs;
using Products.API.Models;

namespace Products.API.Core.MapperProfiles
{
    // The class for mapping profile
    public class ProductMappingProfile: Profile
    {
        // Initializes a new instance of the class
        public ProductMappingProfile() 
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}
