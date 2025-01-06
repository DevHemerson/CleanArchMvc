using CleanArchMvc.Application.Products.Commnads;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers;
public class ProductUpdateComandHandler : IRequestHandler<ProductUpdateCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public ProductUpdateComandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);

        if (product == null) 
        {
            throw new ArgumentException($"Error could not be found.");
        }
        else
        {
            product.Update(request.Name, request.Description, request.Price, request.Stock, request.Image, request.CategoryId);

            return await _productRepository.UpdateAsync(product);
        }
    }
}
