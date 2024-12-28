using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Domain.Entities;
using Mapster;

namespace CleanArchMvc.Application.Mappings;
public static class DomainToDTOMapping
{
    public static void ConfigureMapppings()
    {
        TypeAdapterConfig<Category, CategoryDTO>.NewConfig();
        TypeAdapterConfig<Product, ProductDTO>.NewConfig();
    }
}
