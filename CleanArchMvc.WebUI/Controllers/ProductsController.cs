using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers;
public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductsController(IProductService productAppService, ICategoryService categoryService)
    {
        _productService = productAppService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProducts();
        return View(products);
    }

    [HttpGet()]
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetCategories();
        ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO productDTO)
    {
        if (ModelState.IsValid)
        {
            await _productService.Add(productDTO);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");

        return View(productDTO);
    }
}
