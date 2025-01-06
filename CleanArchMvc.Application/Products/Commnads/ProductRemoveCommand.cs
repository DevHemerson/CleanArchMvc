using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Products.Commnads;
public class ProductRemoveCommand : IRequest<Product>
{
    public int Id { get; set; }
    public ProductRemoveCommand(int id)
    {
        Id = id;
    }
}
