﻿using KupujemProdajem.API.Models;
using KupujemProdajem.Domain.Models;
using KupujemProdajem.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var result = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(result);
        }
        [Authorize (Roles = "admin")]
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdAsync(int categoryId)
        {
            var result = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            return result != null ? Ok(result) : NotFound("Category does not exist");
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryModel categoryCreateModel)
        {
            var categoryModel = new CategoryModel { Name = categoryCreateModel.CategoryName };
            if (!await _categoryRepository.CreateCategory(categoryModel))
            {
                return BadRequest("Something went wrong.");
            }
            return Ok("Category created successfully!");
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!await _categoryRepository.CategoryExists(id))
                return BadRequest("Category does not exist!");
            var categoryToDelete = await _categoryRepository.GetCategoryByIdAsync(id);

            if (!await _categoryRepository.DeleteCategory(categoryToDelete))
            {
                return BadRequest("Something went wrong.");
            }
            return Ok("Category deleted successfully!");
        }
    }
}
