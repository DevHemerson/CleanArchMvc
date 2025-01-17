﻿using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name");
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO productDTO)
    {
        if (ModelState.IsValid)
        {
            // Preenche a propriedade Category com base no CategoryId
            var category = await _categoryService.GetById(productDTO.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError("", "Invalid Category");
                ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name", productDTO.CategoryId);
                return View(productDTO);
            }

            await _productService.Add(productDTO);
            return RedirectToAction(nameof(Index));
        }

        // Recarrega a lista de categorias para a ViewBag caso o ModelState seja inválido
        ViewBag.CategoryId = new SelectList(await _categoryService.GetCategories(), "Id", "Name", productDTO.CategoryId);
        return View(productDTO);
    }


    [HttpGet()]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var productDto = await _productService.GetById(id);
        if (productDto == null) return NotFound();
        var categories = await _categoryService.GetCategories();
        ViewBag.CategoryId = new SelectList(categories, "Id", "Name", productDto.CategoryId);

        return View(productDto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDTO productDTO)
    {
        if (ModelState.IsValid)
        {
            await _productService.Update(productDTO);
            return RedirectToAction(nameof(Index));
        }
        return View(productDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet()]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var productDto = await _productService.GetById(id);
        if (productDto == null) return NotFound();

        return View(productDto);
    }

    [HttpPost(), ActionName("Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.Remove(id);
        return RedirectToAction("Index");
    }

    [HttpGet()]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var productDto = await _productService.GetById(id);
        if (productDto == null) return NotFound();
        return View(productDto);
    }
}
