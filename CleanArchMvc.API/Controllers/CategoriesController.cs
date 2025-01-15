using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
    {
        var categories = await _categoryService.GetCategories();
        if (categories == null) return NotFound("Cateories not found");
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetById(int? id)
    {
        if (id == null) return BadRequest("Invalid Id");
        var category = await _categoryService.GetById(id);
        if (category == null) return NotFound("Category not found");
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CategoryDTO categoryDto)
    {
        if (categoryDto == null) return BadRequest("Invalid Data");
        await _categoryService.Add(categoryDto);
        return new ObjectResult(categoryDto) { StatusCode = StatusCodes.Status201Created };
    }

    [HttpPut]
    public async Task<ActionResult> Update(int id, [FromBody] CategoryDTO categoryDto)
    {
        if (id != categoryDto.Id) return BadRequest("Invalid Data");
        if (categoryDto == null) return BadRequest("Invalid Data");
        await _categoryService.Update(categoryDto);
        return Ok(categoryDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CategoryDTO>> Remove(int? id)
    {
        if (id == null) return BadRequest("Invalid Data");
        var category = _categoryService.GetById(id);
        if (category == null) return NotFound("Category not found");
        await _categoryService.Remove(id);
        return Ok("Category removed");
    }
}
