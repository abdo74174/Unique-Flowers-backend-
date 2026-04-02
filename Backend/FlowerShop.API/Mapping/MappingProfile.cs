using AutoMapper;
using FlowerShop.API.DTOs;
using FlowerShop.API.Models;

namespace FlowerShop.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map Entitiy to DTO
            CreateMap<Product, ProductDto>();

            // Map DTO to Entity
            CreateMap<CreateProductDto, Product>();
            
            // Map DTO to existing Entity for Updates
            CreateMap<CreateProductDto, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
