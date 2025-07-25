namespace Stylo_Spin.Dtos
{
    public class ProductWithCategoryDto
    {
        public int PId { get; set; }
        public string? CategoryName { get; set; }
        public string? PName { get; set; }
        public bool Status { get; set; }
        public string? Description { get; set; }
        public byte[]? ImageData { get; set; }    
        public string? ImageName { get; set; }
        public decimal? Price { get; set; }
        public int ProductQuantity { get; set; }

    }
}
