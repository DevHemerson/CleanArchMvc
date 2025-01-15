using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
    {
        var products = await _productService.GetProducts();
        if (products == null) return NotFound("Products not found");
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetById(int? id)
    {
        if (id == null) return BadRequest("Invalid Id");
        var product = await _productService.GetById(id);
        if (product == null) return NotFound("Product not found");
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ProductDTO productDto)
    {
        if (productDto == null) return BadRequest("Invalid Data");
        await _productService.Add(productDto);
        return new ObjectResult(productDto) { StatusCode = StatusCodes.Status201Created };
    }

    [HttpPut]
    public async Task<ActionResult> Update(int id, [FromBody] ProductDTO productDto)
    {
        if (id != productDto.Id) return BadRequest("Invalid Data");
        if (productDto == null) return BadRequest("Invalid Data");
        await _productService.Update(productDto);
        return Ok(productDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductDTO>> Remove(int? id)
    {
        if (id == null) return BadRequest("Invalid Data");
        var product = _productService.GetById(id);
        if (product == null) return NotFound("Product not found");
        await _productService.Remove(id);
        return Ok("Product removed");
    }
}
