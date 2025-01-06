using CleanArchMvc.Application.Products.Commnads;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers;
public class ProductRemoveComandHandler : IRequestHandler<ProductRemoveCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public ProductRemoveComandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Product> Handle(ProductRemoveCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            throw new ArgumentException($"Error could not be found.");
        }
        else
        {
            var result = await _productRepository.RemoveAsync(product);
            return await _productRepository.UpdateAsync(product);
        }
    }    
}
