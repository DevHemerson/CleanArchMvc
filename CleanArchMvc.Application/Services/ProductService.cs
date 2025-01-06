using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commnads;
using CleanArchMvc.Application.Products.Queries;
using MediatR;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductService(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator ??
            throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productsQuery = new GetProducsQuery() ?? throw new Exception("Invalid operation");
        var products = await _mediator.Send(productsQuery);
        return _mapper.Map<IEnumerable<ProductDTO>>(products);
    }

    public async Task<ProductDTO> GetById(int? id)
    {
        var productQuery = new GetProductByIdQuery(id.Value) ?? throw new Exception("Invalid operation");
        var product = await _mediator.Send(productQuery);
        return _mapper.Map<ProductDTO>(product);
    }

    //public async Task<ProductDTO> GetProductCategory(int? id)
    //{
    //    var productQuery = new GetProductByIdQuery(id.Value) ?? throw new Exception("Invalid operation");
    //    var product = await _mediator.Send(productQuery);
    //    return _mapper.Map<ProductDTO>(product);
    //}

    public async Task Add(ProductDTO productDTO)
    {
        var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
        await _mediator.Send(productCreateCommand);
    }

    public async Task Update(ProductDTO productDTO)
    {
        var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
        await _mediator.Send(productUpdateCommand);
    }

    public async Task Remove(int? id)
    {
        var productRemoveCommand = new ProductRemoveCommand(id.Value);
        await _mediator.Send(productRemoveCommand);
    }
}