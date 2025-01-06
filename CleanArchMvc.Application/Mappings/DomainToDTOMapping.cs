using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Domain.Entities;
using Mapster;

namespace CleanArchMvc.Application.Mappings;
public class DomainToDTOMapping : Profile
{
    public DomainToDTOMapping()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
    }
}
