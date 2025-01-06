using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers;
public class GetProducsQueryHandler : IRequestHandler<GetProducsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _productRepository;
    public GetProducsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IEnumerable<Product>> Handle(GetProducsQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductsAsync();
    }
}
