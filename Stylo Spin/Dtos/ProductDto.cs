using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Stylo_Spin.Dtos
{
    public class ProductDto
    {
        [Required(ErrorMessage = "Category name is required.")]
        public string C_Name { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name can't exceed 100 characters.")]
        public string? PName { get; set; }

        [Required(ErrorMessage = "Product status is required.")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(500, ErrorMessage = "Description can't exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Product image is required.")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Product quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int ProductQuantity { get; set; }
    }
}
