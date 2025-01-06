using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Products.Commnads;
using Mapster;

namespace CleanArchMvc.Application.Mappings;

public class DTOToCommandMapping : Profile
{
    public DTOToCommandMapping()
    {
        CreateMap<ProductDTO, ProductCreateCommand>();
        CreateMap<ProductDTO, ProductUpdateCommand>();
    }
}
