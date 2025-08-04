using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stylo_Spin.Dtos;
using Stylo_Spin.Models;
using Stylo_Spin.Services.Defination;

namespace Stylo_Spin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
 
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
     
        public async Task<IActionResult> CreateProduct([FromForm] ProductDto dto)
        {
            var product = await _service.CreateProductAsync(dto);
            if (product == null)
                return BadRequest(ApiResponse<string>.ErrorResponse("Product creation failed. Check if category exists."));

            return CreatedAtAction(nameof(GetProductById),
                new { id = product.PId },
                ApiResponse<TblProduct>.SuccessResponse(product, "Product created successfully"));
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDto dto)
        {
            var updated = await _service.UpdateProductAsync(id, dto);
            if (updated == null)
                return NotFound(ApiResponse<string>.ErrorResponse("Product not found for update."));

            return Ok(ApiResponse<TblProduct>.SuccessResponse(updated, "Product updated successfully"));
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool result = await _service.DeleteProductAsync(id);
            if (!result)
                return NotFound(ApiResponse<string>.ErrorResponse("Product not found for deletion."));

            return Ok(ApiResponse<string>.SuccessResponse("Product deleted successfully"));
        }

        [HttpGet("list")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _service.GetAllProductsAsync();
            var productDtos = products.Select(p => new ProductWithCategoryDto
            {
                PId = p.PId,
                CategoryName = p.CIdNavigation?.CName,   // Fetch from navigation property
                PName = p.PName,
                Status = p.Status,
                Description = p.Description,
                ImageData = p.ImageData ?? null,
                ImageName = p.ImageName,
                Price = p.Price,
                ProductQuantity = p.ProductQuantity
            }).ToList();
            return Ok(ApiResponse<List<ProductWithCategoryDto>>.SuccessResponse(productDtos, "All products retrieved successfully"));
        }

        [HttpGet("get/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null)
                return NotFound(ApiResponse<string>.ErrorResponse("Product not found."));

            return Ok(ApiResponse<TblProduct>.SuccessResponse(product, "Product retrieved successfully"));
        }

        [HttpGet("category/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _service.GetProductsByCategoryAsync(categoryId);
            return Ok(ApiResponse<List<TblProduct>>.SuccessResponse(products, "Products filtered by category"));
        }

        [HttpGet("price")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductsByPrice([FromQuery] decimal min, [FromQuery] decimal max)
        {
            var products = await _service.GetProductsByPriceRangeAsync(min, max);
            return Ok(ApiResponse<List<TblProduct>>.SuccessResponse(products, "Products filtered by price range"));
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchProducts([FromQuery] string term)
        {
            var products = await _service.SearchProductsAsync(term);
            return Ok(ApiResponse<List<TblProduct>>.SuccessResponse(products, $"Search results for '{term}'"));
        }
        [HttpGet("image/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductImage(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null || product.ImageData == null)
                return NotFound(ApiResponse<string>.ErrorResponse("Image not found."));

            return File(product.ImageData, "image/jpeg"); // Adjust MIME type as needed
        }
    }
}
