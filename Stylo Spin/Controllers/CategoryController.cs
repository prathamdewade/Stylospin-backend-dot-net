using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stylo_Spin.Dtos;
using Stylo_Spin.Helper;
using Stylo_Spin.Models;
using Stylo_Spin.Services.Defination;

namespace Stylo_Spin.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICatgoryService _service;

        public CategoryController(ICatgoryService service)
        {
            _service = service;
        }

        /// <summary>
        /// Add a new category.
        /// </summary>
        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.CName))
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid category data"));

            var result = await _service.CreateCategoryAsync(dto);
            if (!result)
                return Conflict(ApiResponse<string>.ErrorResponse("Category with this name already exists"));

            return Ok(ApiResponse<string>.SuccessResponse("Category created successfully"));
        }


        /// <summary>
        /// Get all categories.
        /// </summary>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<TblCategory>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _service.GetAllCategoriesAsync();
            if (categories == null || !categories.Any())
                return NotFound(ApiResponse<string>.ErrorResponse("No categories found"));

            return Ok(ApiResponse<IEnumerable<TblCategory>>.SuccessResponse(categories, "Categories retrieved successfully"));
        }

        /// <summary>
        /// Update an existing category by ID.
        /// </summary>
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.CName))
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid category data"));

            var result = await _service.UpdateCategoryAsync(id, dto);
            if (!result)
                return NotFound(ApiResponse<string>.ErrorResponse("Category not found or update failed"));

            return Ok(ApiResponse<string>.SuccessResponse("Category updated successfully"));
        }
        /// <summary>
        /// Delete a category by ID.
        /// </summary>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _service.DeleteCategoryAsync(id);
            if (!result)
                return NotFound(ApiResponse<string>.ErrorResponse("Category not found or delete failed"));

            return Ok(ApiResponse<int>.SuccessResponse(id, "Category deleted successfully"));
        }
    }
}
