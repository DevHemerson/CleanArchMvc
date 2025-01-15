using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Products.Commnads;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<ProductCreateCommand, ProductDTO>().ReverseMap();
        CreateMap<ProductUpdateCommand, ProductDTO>().ReverseMap();
    }
}

