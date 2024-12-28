using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MapsterMapper;

namespace CleanArchMvc.Application.Services;
public class CategoryService : ICategoryService
{
    private readonly ICategoryReposiory _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryReposiory categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategories()
    {
        var categoryEntities = await _categoryRepository.GetCategories();
        return _mapper.Map<IEnumerable<CategoryDTO>>(categoryEntities);
    }
    public async Task<CategoryDTO> GetById(int? id)
    {
        var categoruEntity = await _categoryRepository.GetById(id);
        return _mapper.Map<CategoryDTO>(categoruEntity);
    }
    public async Task Add(CategoryDTO categoryDto)
    {
        var categoryEntity = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.Create(categoryEntity);
    }
    public async Task Update(CategoryDTO categoryDto)
    {
        var entityCategory = _mapper.Map<Category>(categoryDto);
        await _categoryRepository.Update(entityCategory);
    }
    public async Task Remove(int? id)
    {
        var categoryEntity = _categoryRepository.GetById(id).Result;
        await _categoryRepository.Remove(categoryEntity);
    }
}
